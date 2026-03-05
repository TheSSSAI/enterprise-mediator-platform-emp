'use client';

import React, { useState, useEffect, useCallback, useTransition } from 'react';
import { useRouter } from 'next/navigation';
import { z } from 'zod';

// Level 0: Types and Schemas
import { ProjectDTO, ProjectStatus, SowData } from '@/lib/types';
import { SowValidationSchema } from '@/lib/schemas';

// Level 1: Stores
import { useNotificationStore } from '@/store/use-notification-store';

// Level 3: Actions
import { updateProjectBrief, approveProjectBrief } from '@/actions/project.actions';

// Level 4: Feature Components
import { SowExtractionForm } from '@/components/features/sow/SowExtractionForm';
import { SanitizedSowViewer } from '@/components/features/sow/SanitizedSowViewer';
import { ComparisonSplitView } from '@/components/features/sow/ComparisonSplitView';

// UI Components (assuming availability from a UI library wrapper or standard HTML elements for layout)
import { Button } from '@/components/ui/button'; // Assuming generic UI component availability
import { 
  Dialog, 
  DialogContent, 
  DialogHeader, 
  DialogTitle, 
  DialogDescription, 
  DialogFooter 
} from '@/components/ui/dialog'; // Assuming generic UI component availability

interface SowReviewCompositeProps {
  project: ProjectDTO;
}

/**
 * SowReviewComposite
 * 
 * A "Human-in-the-Loop" orchestration component for reviewing, editing, and approving
 * AI-extracted Statement of Work data.
 * 
 * Features:
 * - Side-by-side view of Sanitized SOW and Extraction Form
 * - Dirty state tracking and "unsaved changes" warnings
 * - Optimistic updates and Server Action integration
 * - Validation prior to approval
 */
export function SowReviewComposite({ project }: SowReviewCompositeProps) {
  const router = useRouter();
  const { addNotification } = useNotificationStore();
  const [isPending, startTransition] = useTransition();

  // -- State Management --
  
  // Initialize form data from project or default empty state
  const [formData, setFormData] = useState<SowData>(() => {
    return project.sowData || {
      scopeSummary: '',
      deliverables: [],
      requiredSkills: [],
      timeline: { startDate: undefined, endDate: undefined, milestones: [] },
      technologies: [],
      budget: { currency: 'USD', min: 0, max: 0 }
    };
  });

  // Track initial state for dirty checking
  const [initialData, setInitialData] = useState<string>(JSON.stringify(formData));
  const [isDirty, setIsDirty] = useState(false);
  
  // Validation errors
  const [errors, setErrors] = useState<Record<string, string[]>>({});
  
  // Modal state
  const [showApproveModal, setShowApproveModal] = useState(false);

  // -- Effects --

  // Update dirty state whenever formData changes
  useEffect(() => {
    const currentString = JSON.stringify(formData);
    setIsDirty(currentString !== initialData);
  }, [formData, initialData]);

  // Warn user before unloading if changes are unsaved
  useEffect(() => {
    const handleBeforeUnload = (e: BeforeUnloadEvent) => {
      if (isDirty) {
        e.preventDefault();
        e.returnValue = '';
      }
    };

    window.addEventListener('beforeunload', handleBeforeUnload);
    return () => window.removeEventListener('beforeunload', handleBeforeUnload);
  }, [isDirty]);

  // -- Handlers --

  const handleFieldChange = useCallback((field: keyof SowData, value: any) => {
    setFormData((prev) => ({
      ...prev,
      [field]: value,
    }));
    // Clear errors for the modified field
    if (errors[field]) {
      setErrors((prev) => {
        const newErrors = { ...prev };
        delete newErrors[field];
        return newErrors;
      });
    }
  }, [errors]);

  const handleSave = async () => {
    // Basic validation for Save (less strict than Approve)
    // We assume we can save drafts even if incomplete
    
    startTransition(async () => {
      try {
        const result = await updateProjectBrief(project.id, formData);
        
        if (result.success) {
          setInitialData(JSON.stringify(formData));
          setIsDirty(false);
          addNotification({
            type: 'success',
            title: 'Changes Saved',
            message: 'The project brief has been updated successfully.',
          });
          router.refresh(); // Refresh server components to reflect changes if needed
        } else {
          addNotification({
            type: 'error',
            title: 'Save Failed',
            message: result.error || 'An unexpected error occurred while saving.',
          });
        }
      } catch (error) {
        addNotification({
          type: 'error',
          title: 'System Error',
          message: 'Failed to communicate with the server.',
        });
      }
    });
  };

  const handleApproveClick = () => {
    // Strict Validation using Zod Schema before opening confirmation
    const validationResult = SowValidationSchema.safeParse(formData);
    
    if (!validationResult.success) {
      const formattedErrors: Record<string, string[]> = {};
      validationResult.error.errors.forEach((err) => {
        const path = err.path[0] as string;
        if (!formattedErrors[path]) formattedErrors[path] = [];
        formattedErrors[path].push(err.message);
      });
      
      setErrors(formattedErrors);
      addNotification({
        type: 'error',
        title: 'Validation Failed',
        message: 'Please fix the highlighted errors before approving the brief.',
      });
      return;
    }

    // If valid, show confirmation modal
    setShowApproveModal(true);
  };

  const confirmApproval = async () => {
    setShowApproveModal(false);
    
    startTransition(async () => {
      try {
        // First ensure latest data is saved
        if (isDirty) {
          const saveResult = await updateProjectBrief(project.id, formData);
          if (!saveResult.success) {
            throw new Error(saveResult.error || 'Failed to save final changes before approval.');
          }
        }

        // Then approve
        const result = await approveProjectBrief(project.id);
        
        if (result.success) {
          addNotification({
            type: 'success',
            title: 'Project Brief Approved',
            message: 'The brief is now locked and vendor matching has started.',
          });
          // Redirect or refresh to update view state (likely to a read-only view)
          router.refresh(); 
        } else {
          addNotification({
            type: 'error',
            title: 'Approval Failed',
            message: result.error || 'Could not approve the project brief.',
          });
        }
      } catch (error) {
        addNotification({
          type: 'error',
          title: 'Operation Failed',
          message: error instanceof Error ? error.message : 'An unknown error occurred.',
        });
      }
    });
  };

  // -- Derived State --
  const isReadOnly = project.status !== 'Pending' && project.status !== 'Proposed'; // Assuming logic for when edits are allowed
  const hasSanitizedDoc = !!project.sowSanitizedUrl;

  return (
    <div className="flex flex-col h-full w-full bg-background">
      {/* Header / Toolbar */}
      <div className="flex items-center justify-between px-6 py-4 border-b bg-card">
        <div>
          <h2 className="text-2xl font-semibold tracking-tight">Review Project Brief</h2>
          <p className="text-sm text-muted-foreground">
            Verify AI-extracted data against the sanitized SOW.
          </p>
        </div>
        <div className="flex items-center gap-3">
          <div className="text-sm text-muted-foreground mr-2">
            {isDirty ? (
              <span className="text-amber-500 font-medium">Unsaved Changes</span>
            ) : (
              <span className="text-green-600">All changes saved</span>
            )}
          </div>
          
          {!isReadOnly && (
            <>
              <Button 
                variant="outline" 
                onClick={handleSave} 
                disabled={!isDirty || isPending}
              >
                {isPending ? 'Saving...' : 'Save Draft'}
              </Button>
              <Button 
                variant="default" 
                onClick={handleApproveClick} 
                disabled={isPending}
              >
                {isPending ? 'Processing...' : 'Approve & Finalize'}
              </Button>
            </>
          )}
        </div>
      </div>

      {/* Main Content: Split View */}
      <div className="flex-1 overflow-hidden">
        <ComparisonSplitView
          leftPanel={
            <div className="h-full overflow-y-auto p-6">
              <SowExtractionForm
                data={formData}
                onChange={handleFieldChange}
                errors={errors}
                readOnly={isReadOnly || isPending}
              />
            </div>
          }
          rightPanel={
            <div className="h-full overflow-y-auto bg-muted/30 border-l">
              <SanitizedSowViewer
                documentUrl={project.sowSanitizedUrl}
                projectId={project.id}
                isLoading={false} // Assuming data is prefetched by page
                hasDocument={hasSanitizedDoc}
              />
            </div>
          }
          initialSplitRatio={50} // 50/50 split by default
        />
      </div>

      {/* Approval Confirmation Modal */}
      <Dialog open={showApproveModal} onOpenChange={setShowApproveModal}>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Approve Project Brief?</DialogTitle>
            <DialogDescription>
              This action will finalize the Project Brief and lock it for editing. 
              The system will immediately begin matching vendors based on these requirements.
              <br /><br />
              <strong>This action cannot be undone.</strong>
            </DialogDescription>
          </DialogHeader>
          <DialogFooter>
            <Button variant="outline" onClick={() => setShowApproveModal(false)}>
              Cancel
            </Button>
            <Button variant="default" onClick={confirmApproval}>
              Confirm Approval
            </Button>
          </DialogFooter>
        </DialogContent>
      </Dialog>
    </div>
  );
}