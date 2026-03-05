'use client';

import React, { useState, useTransition } from 'react';
import { Dialog } from '@headlessui/react'; // Assuming HeadlessUI or similar for a11y primitives
import { formatCurrency } from '@/lib/utils';
import { ExclamationTriangleIcon, XMarkIcon } from '@heroicons/react/24/outline';
import { approvePayout, rejectPayout } from '@/actions/finance.actions'; // Level 3

interface PayoutApprovalModalProps {
  payoutId: string;
  vendorName: string;
  amount: number;
  currency: string;
  projectTitle: string;
  isOpen: boolean;
  onClose: () => void;
  onActionComplete: () => void;
}

/**
 * PayoutApprovalModal Component
 * 
 * Interactive dialog for Finance Managers to approve or reject pending vendor payouts.
 * Includes safety checks and mandatory reasoning for rejection.
 */
export function PayoutApprovalModal({
  payoutId,
  vendorName,
  amount,
  currency,
  projectTitle,
  isOpen,
  onClose,
  onActionComplete
}: PayoutApprovalModalProps) {
  const [isPending, startTransition] = useTransition();
  const [rejectReason, setRejectReason] = useState('');
  const [mode, setMode] = useState<'VIEW' | 'REJECTING'>('VIEW');
  const [error, setError] = useState<string | null>(null);

  const handleApprove = () => {
    startTransition(async () => {
      try {
        const result = await approvePayout(payoutId);
        if (result.success) {
          onActionComplete();
          onClose();
        } else {
          setError(result.error || 'Approval failed');
        }
      } catch (err) {
        setError('System error during approval');
      }
    });
  };

  const handleReject = () => {
    if (!rejectReason.trim()) {
      setError('Rejection reason is required');
      return;
    }
    
    startTransition(async () => {
      try {
        const result = await rejectPayout(payoutId, rejectReason);
        if (result.success) {
          onActionComplete();
          onClose();
        } else {
          setError(result.error || 'Rejection failed');
        }
      } catch (err) {
        setError('System error during rejection');
      }
    });
  };

  if (!isOpen) return null;

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/50 backdrop-blur-sm">
      <div className="bg-white rounded-xl shadow-2xl max-w-md w-full overflow-hidden animate-in fade-in zoom-in duration-200">
        
        {/* Header */}
        <div className="px-6 py-4 bg-slate-50 border-b border-slate-100 flex justify-between items-center">
          <h3 className="text-lg font-semibold text-slate-900">
            {mode === 'REJECTING' ? 'Reject Payout' : 'Approve Payout'}
          </h3>
          <button onClick={onClose} className="text-slate-400 hover:text-slate-600">
            <XMarkIcon className="h-6 w-6" />
          </button>
        </div>

        {/* Content */}
        <div className="p-6">
          {error && (
            <div className="mb-4 p-3 bg-red-50 text-red-700 text-sm rounded border border-red-200 flex items-center">
              <ExclamationTriangleIcon className="h-5 w-5 mr-2" />
              {error}
            </div>
          )}

          {mode === 'VIEW' ? (
            <div className="space-y-4">
              <div className="bg-blue-50 p-4 rounded-lg text-center">
                <p className="text-sm text-blue-600 mb-1">Total Payout Amount</p>
                <p className="text-3xl font-bold text-blue-900">{formatCurrency(amount, currency)}</p>
              </div>
              
              <div className="text-sm text-slate-600 space-y-2">
                <div className="flex justify-between">
                  <span>Vendor:</span>
                  <span className="font-medium text-slate-900">{vendorName}</span>
                </div>
                <div className="flex justify-between">
                  <span>Project:</span>
                  <span className="font-medium text-slate-900">{projectTitle}</span>
                </div>
              </div>

              <p className="text-xs text-slate-500 mt-4">
                By approving, you authorize the release of escrowed funds to the vendor immediately. This action is logged.
              </p>
            </div>
          ) : (
            <div className="space-y-4">
              <p className="text-sm text-slate-600">
                Please provide a reason for rejecting this payout. This will be sent to the vendor.
              </p>
              <textarea
                value={rejectReason}
                onChange={(e) => setRejectReason(e.target.value)}
                className="w-full h-32 rounded-md border-slate-300 shadow-sm focus:border-red-500 focus:ring-red-500 sm:text-sm p-3 border"
                placeholder="e.g., Deliverables incomplete, Invoice discrepancy..."
              />
            </div>
          )}
        </div>

        {/* Footer */}
        <div className="px-6 py-4 bg-slate-50 border-t border-slate-100 flex justify-end gap-3">
          {mode === 'VIEW' ? (
            <>
              <button
                onClick={() => setMode('REJECTING')}
                disabled={isPending}
                className="px-4 py-2 text-sm font-medium text-red-600 hover:bg-red-50 rounded-md transition-colors"
              >
                Reject
              </button>
              <button
                onClick={handleApprove}
                disabled={isPending}
                className="px-4 py-2 text-sm font-medium text-white bg-green-600 hover:bg-green-700 rounded-md shadow-sm transition-colors flex items-center"
              >
                {isPending ? 'Processing...' : 'Confirm Approval'}
              </button>
            </>
          ) : (
            <>
              <button
                onClick={() => { setMode('VIEW'); setError(null); }}
                disabled={isPending}
                className="px-4 py-2 text-sm font-medium text-slate-600 hover:bg-slate-100 rounded-md"
              >
                Back
              </button>
              <button
                onClick={handleReject}
                disabled={isPending}
                className="px-4 py-2 text-sm font-medium text-white bg-red-600 hover:bg-red-700 rounded-md shadow-sm"
              >
                {isPending ? 'Rejecting...' : 'Confirm Rejection'}
              </button>
            </>
          )}
        </div>
      </div>
    </div>
  );
}