'use server';

import { revalidatePath, revalidateTag } from 'next/cache';
import { FinanceService } from '@/services/finance.service';
import { InvoiceSchema } from '@/lib/schemas';

type FinanceActionState = {
  success: boolean;
  message?: string;
  data?: any;
  errors?: Record<string, string[]>;
};

const financeService = new FinanceService();

/**
 * Generates and sends an invoice for a specific project.
 */
export async function createInvoiceAction(
  projectId: string,
  data: any
): Promise<FinanceActionState> {
  try {
    const validated = InvoiceSchema.safeParse(data);
    
    if (!validated.success) {
      return {
        success: false,
        errors: validated.error.flatten().fieldErrors,
        message: 'Invalid invoice data.'
      };
    }

    const invoice = await financeService.createInvoice(projectId, validated.data);
    
    revalidatePath(`/admin/projects/${projectId}`);
    return { success: true, data: invoice, message: 'Invoice generated and sent.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to create invoice.' };
  }
}

/**
 * Initiates a payout workflow for a vendor.
 */
export async function initiatePayoutAction(
  projectId: string,
  milestoneId: string | null,
  amount: number
): Promise<FinanceActionState> {
  try {
    if (amount <= 0) {
      return { success: false, message: 'Payout amount must be greater than zero.' };
    }

    await financeService.initiatePayout({ projectId, milestoneId, amount });
    
    revalidatePath('/admin/finance/payouts');
    revalidatePath(`/admin/projects/${projectId}`);
    
    return { success: true, message: 'Payout initiated successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to initiate payout.' };
  }
}

/**
 * Approves a pending payout request.
 */
export async function approvePayoutAction(payoutId: string): Promise<FinanceActionState> {
  try {
    await financeService.approvePayout(payoutId);
    revalidatePath('/admin/finance/payouts');
    return { success: true, message: 'Payout approved successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to approve payout.' };
  }
}

/**
 * Rejects a pending payout request.
 */
export async function rejectPayoutAction(
  payoutId: string, 
  reason: string
): Promise<FinanceActionState> {
  try {
    if (!reason) return { success: false, message: 'Rejection reason is required.' };

    await financeService.rejectPayout(payoutId, reason);
    revalidatePath('/admin/finance/payouts');
    return { success: true, message: 'Payout rejected.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to reject payout.' };
  }
}

/**
 * Processes a refund for a project.
 */
export async function processRefundAction(
  projectId: string,
  amount: number,
  reason: string
): Promise<FinanceActionState> {
  try {
    await financeService.processRefund(projectId, amount, reason);
    revalidatePath(`/admin/projects/${projectId}`);
    return { success: true, message: 'Refund processed successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to process refund.' };
  }
}