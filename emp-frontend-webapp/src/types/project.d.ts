export type ProjectStatus =
  | 'Pending'
  | 'Proposed'
  | 'Awarded'
  | 'Active'
  | 'Completed'
  | 'OnHold'
  | 'Cancelled'
  | 'Disputed';

export type SowStatus =
  | 'Pending'
  | 'Processing'
  | 'Processed'
  | 'Failed';

export interface ProjectDTO {
  id: string;
  name: string;
  clientId: string;
  clientName: string;
  status: ProjectStatus;
  description?: string;
  createdAt: string;
  updatedAt: string;
  vendorId?: string;
  vendorName?: string;
  startDate?: string;
  endDate?: string;
  budget?: number;
}

export interface SowData {
  id: string;
  projectId: string;
  originalFileName: string;
  fileUrl: string;
  status: SowStatus;
  uploadedBy: string;
  uploadedAt: string;
  processedAt?: string;
  // Extracted Data
  extractedData?: {
    scopeSummary: string;
    deliverables: string[];
    requiredSkills: string[];
    technologies: string[];
    timeline: string;
    confidenceScore: number;
  };
  sanitizedContent?: string;
  errorDetails?: string;
}

export interface VendorRecommendation {
  vendorId: string;
  companyName: string;
  similarityScore: number; // 0-100
  matchedSkills: string[];
  matchReasoning?: string;
}

export interface ProjectBrief {
  id: string;
  projectId: string;
  content: string; // Finalized scope
  requirements: string[];
  budgetRange?: string;
  timelineEstimate?: string;
  approvedAt: string;
  approvedBy: string;
}