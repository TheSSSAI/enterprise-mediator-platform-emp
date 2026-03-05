'use client';

import React, { useState } from 'react';
import { loadStripe } from '@stripe/stripe-js';
import {
  Elements,
  PaymentElement,
  useStripe,
  useElements
} from '@stripe/react-stripe-js';
import { processInvoicePayment } from '@/actions/finance.actions'; // Level 3
import { LockClosedIcon } from '@heroicons/react/24/solid';

// Initialize Stripe outside component to avoid recreation
const stripePromise = loadStripe(process.env.NEXT_PUBLIC_STRIPE_PUBLISHABLE_KEY!);

interface InvoicePaymentFormProps {
  invoiceId: string;
  clientSecret: string;
  amount: number;
  currency: string;
}

function PaymentFormContent({ invoiceId, amount, currency }: Omit<InvoicePaymentFormProps, 'clientSecret'>) {
  const stripe = useStripe();
  const elements = useElements();
  const [errorMessage, setErrorMessage] = useState<string | null>(null);
  const [isProcessing, setIsProcessing] = useState(false);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (!stripe || !elements) {
      return; // Stripe.js hasn't loaded yet
    }

    setIsProcessing(true);
    setErrorMessage(null);

    // 1. Confirm Payment with Stripe
    const { error, paymentIntent } = await stripe.confirmPayment({
      elements,
      confirmParams: {
        return_url: `${window.location.origin}/pay/confirm?invoice=${invoiceId}`,
      },
      redirect: 'if_required',
    });

    if (error) {
      setErrorMessage(error.message ?? 'An unknown error occurred');
      setIsProcessing(false);
    } else if (paymentIntent && paymentIntent.status === 'succeeded') {
      // 2. Notify Backend of Success (if no redirect)
      const result = await processInvoicePayment(invoiceId, paymentIntent.id);
      if (result.success) {
        window.location.href = `/pay/success?invoice=${invoiceId}`;
      } else {
        setErrorMessage('Payment succeeded but system update failed. Please contact support.');
        setIsProcessing(false);
      }
    }
  };

  return (
    <form onSubmit={handleSubmit} className="w-full max-w-md mx-auto p-6 bg-white rounded-xl shadow-lg border border-slate-200">
      <div className="mb-6 text-center">
        <h2 className="text-xl font-bold text-slate-900">Secure Payment</h2>
        <p className="text-slate-500 mt-1">Total due: <span className="text-slate-900 font-semibold">{new Intl.NumberFormat('en-US', { style: 'currency', currency }).format(amount)}</span></p>
      </div>

      <div className="mb-6">
        <PaymentElement />
      </div>

      {errorMessage && (
        <div className="mb-4 p-3 bg-red-50 border border-red-200 text-red-600 text-sm rounded">
          {errorMessage}
        </div>
      )}

      <button
        disabled={!stripe || isProcessing}
        className="w-full flex justify-center items-center py-3 px-4 border border-transparent rounded-md shadow-sm text-sm font-medium text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50 disabled:cursor-not-allowed transition-all"
      >
        {isProcessing ? (
          <span className="flex items-center">
            <svg className="animate-spin -ml-1 mr-3 h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
              <circle className="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" strokeWidth="4"></circle>
              <path className="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
            </svg>
            Processing...
          </span>
        ) : (
          <span className="flex items-center">
            <LockClosedIcon className="h-4 w-4 mr-2" />
            Pay Now
          </span>
        )}
      </button>
      
      <div className="mt-4 flex justify-center items-center space-x-2 text-xs text-slate-400">
        <LockClosedIcon className="h-3 w-3" />
        <span>Payments processed securely by Stripe</span>
      </div>
    </form>
  );
}

/**
 * InvoicePaymentForm Wrapper
 * 
 * Sets up the Stripe Elements provider with the client secret fetched from the backend.
 */
export function InvoicePaymentForm(props: InvoicePaymentFormProps) {
  const options = {
    clientSecret: props.clientSecret,
    appearance: {
      theme: 'stripe',
      variables: {
        colorPrimary: '#2563eb',
      },
    },
  } as const;

  return (
    <Elements stripe={stripePromise} options={options}>
      <PaymentFormContent {...props} />
    </Elements>
  );
}