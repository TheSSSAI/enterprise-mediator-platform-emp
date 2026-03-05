import { z } from 'zod';
import { ACCEPTED_FILE_TYPES, FILE_UPLOAD_MAX_SIZE } from './constants';

/**
 * Authentication Schemas
 */
export const loginSchema = z.object({
  email: z.string().email('Please enter a valid email address'),
  password: [REDACTED] 'Password must be at least 8 characters'),
});

export const mfaVerifySchema = z.object({
  code: z
    .string()
    .length(6, 'Code must be exactly 6 digits')
    .regex(/^\d+$/, 'Code must contain only numbers'),
});

/**
 * Project Schemas
 */
export const createProjectSchema = z.object({
  name: z.string().min(3, 'Project name must be at least 3 characters'),
  clientId: z.string().uuid('Please select a valid client'),
  description: z.string().optional(),
});

/**
 * SOW Upload Schemas
 */
export const sowUploadSchema = z.object({
  file: z
    .custom<File>((val) => val instanceof File, 'Please upload a file')
    .refine(
      (file) => file.size <= FILE_UPLOAD_MAX_SIZE,
      `File size must be less than ${FILE_UPLOAD_MAX_SIZE / 1024 / 1024}MB`
    )
    .refine(
      (file) => ACCEPTED_FILE_TYPES.includes(file.type),
      'Only .pdf and .docx formats are supported'
    ),
});

/**
 * Vendor Schemas
 */
export const vendorProfileSchema = z.object({
  companyName: z.string().min(2, 'Company name is required'),
  address: z.string().min(5, 'Address is required'),
  primaryContactName: z.string().min(2, 'Contact name is required'),
  primaryContactEmail: z.string().email('Invalid email address'),
  primaryContactPhone: z.string().min(10, 'Valid phone number is required'),
  skills: z.array(z.string()).min(1, 'At least one skill is required'),
  paymentDetails: z.object({
    bankName: z.string().min(2, 'Bank name is required'),
    accountNumber: z.string().min(8, 'Account number is required'),
    swiftCode: z.string().min(8, 'SWIFT code is required'),
  }).optional(),
});

/**
 * Client Schemas
 */
export const clientSchema = z.object({
  companyName: z.string().min(2, 'Company name is required'),
  address: z.string().min(5, 'Address is required'),
  contacts: z.array(
    z.object({
      name: z.string().min(2, 'Contact name is required'),
      email: z.string().email('Invalid email address'),
      phone: z.string().optional(),
    })
  ).min(1, 'At least one contact is required'),
});

/**
 * Proposal Schemas
 */
export const proposalSubmissionSchema = z.object({
  cost: z.coerce.number().positive('Cost must be a positive number'),
  timeline: z.string().min(5, 'Timeline description is required'),
  keyPersonnel: z.string().min(10, 'Please list key personnel'),
  file: z.optional(
    z.custom<File>((val) => val instanceof File, 'Invalid file')
      .refine(
        (file) => file.size <= FILE_UPLOAD_MAX_SIZE,
        `File size limit is ${FILE_UPLOAD_MAX_SIZE / 1024 / 1024}MB`
      )
  ),
});

export type LoginFormData = z.infer<typeof loginSchema>;
export type MfaVerifyFormData = z.infer<typeof mfaVerifySchema>;
export type CreateProjectFormData = z.infer<typeof createProjectSchema>;
export type SowUploadFormData = z.infer<typeof sowUploadSchema>;
export type VendorProfileFormData = z.infer<typeof vendorProfileSchema>;
export type ClientFormData = z.infer<typeof clientSchema>;
export type ProposalSubmissionFormData = z.infer<typeof proposalSubmissionSchema>;