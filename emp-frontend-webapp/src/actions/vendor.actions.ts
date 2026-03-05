'use server';

import { revalidatePath, revalidateTag } from 'next/cache';
import { z } from 'zod';
import { VendorService } from '@/services/vendor.service';
import { VendorSchema } from '@/lib/schemas';
import { VendorDTO } from '@/lib/types';

type VendorActionState = {
  success: boolean;
  message?: string;
  data?: VendorDTO;
  errors?: Record<string, string[]>;
};

const vendorService = new VendorService();

/**
 * Creates a new vendor profile.
 */
export async function createVendorAction(
  prevState: any,
  formData: FormData
): Promise<VendorActionState> {
  try {
    const rawData = Object.fromEntries(formData.entries());
    
    // Parse skills from string/JSON if necessary based on form structure
    if (typeof rawData.skills === 'string') {
      try {
        rawData.skills = JSON.parse(rawData.skills);
      } catch {
        rawData.skills = [];
      }
    }

    const validated = VendorSchema.safeParse(rawData);

    if (!validated.success) {
      return {
        success: false,
        errors: validated.error.flatten().fieldErrors,
        message: 'Validation failed.'
      };
    }

    const newVendor = await vendorService.createVendor(validated.data);
    
    revalidateTag('vendors');
    revalidatePath('/admin/vendors');

    return { success: true, data: newVendor, message: 'Vendor created successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to create vendor.' };
  }
}

/**
 * Updates an existing vendor profile.
 */
export async function updateVendorAction(
  vendorId: string,
  data: Partial<VendorDTO>
): Promise<VendorActionState> {
  try {
    const updatedVendor = await vendorService.updateVendor(vendorId, data);
    
    revalidatePath(`/admin/vendors/${vendorId}`);
    revalidateTag(`vendor-${vendorId}`);

    return { success: true, data: updatedVendor, message: 'Vendor profile updated.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to update vendor.' };
  }
}

/**
 * Activates a pending vendor.
 */
export async function activateVendorAction(vendorId: string): Promise<VendorActionState> {
  try {
    await vendorService.updateStatus(vendorId, 'Active');
    revalidatePath('/admin/vendors');
    revalidatePath(`/admin/vendors/${vendorId}`);
    return { success: true, message: 'Vendor activated successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to activate vendor.' };
  }
}

/**
 * Deactivates a vendor.
 */
export async function deactivateVendorAction(vendorId: string): Promise<VendorActionState> {
  try {
    await vendorService.updateStatus(vendorId, 'Deactivated');
    revalidatePath('/admin/vendors');
    revalidatePath(`/admin/vendors/${vendorId}`);
    return { success: true, message: 'Vendor deactivated.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to deactivate vendor.' };
  }
}

/**
 * Invites a new contact to the vendor account.
 */
export async function inviteVendorContactAction(
  vendorId: string,
  email: string
): Promise<VendorActionState> {
  try {
    const emailSchema = z.string().email();
    const result = emailSchema.safeParse(email);
    
    if (!result.success) {
      return { success: false, message: 'Invalid email address.' };
    }

    await vendorService.inviteContact(vendorId, email);
    revalidatePath(`/admin/vendors/${vendorId}`);
    
    return { success: true, message: `Invitation sent to ${email}.` };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to invite contact.' };
  }
}