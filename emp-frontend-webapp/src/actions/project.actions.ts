'use server';

import { revalidatePath, revalidateTag } from 'next/cache';
import { z } from 'zod';
import { ProjectService } from '@/services/project.service';
import { SowValidationSchema } from '@/lib/schemas';
import { ProjectDTO, CreateProjectRequest } from '@/lib/types';

// Types for Action Responses
type ProjectActionState = {
  success: boolean;
  message?: string;
  data?: ProjectDTO;
  errors?: Record<string, string[]>;
};

const projectService = new ProjectService();

/**
 * Creates a new project based on validated input.
 */
export async function createProjectAction(
  data: CreateProjectRequest
): Promise<ProjectActionState> {
  try {
    // Server-side validation
    if (!data.name || !data.clientId) {
      return { success: false, message: 'Project name and client are required.' };
    }

    const newProject = await projectService.createProject(data);
    
    // Invalidate cache to show new project in lists
    revalidateTag('projects');
    revalidatePath('/admin/dashboard');

    return { success: true, data: newProject, message: 'Project created successfully.' };
  } catch (error: any) {
    console.error('Create Project Action Error:', error);
    return { success: false, message: error.message || 'Failed to create project.' };
  }
}

/**
 * Handles the upload of an SOW document.
 * Processes FormData, validates file constraints, and delegates to service.
 */
export async function uploadSowAction(
  projectId: string,
  formData: FormData
): Promise<ProjectActionState> {
  try {
    const file = formData.get('file') as File;

    if (!file) {
      return { success: false, message: 'No file provided.' };
    }

    // 1. Validate File Constraints (redundant check for security)
    const validationResult = SowValidationSchema.safeParse({
      size: file.size,
      type: file.type,
      name: file.name
    });

    if (!validationResult.success) {
      return { 
        success: false, 
        message: 'File validation failed.', 
        errors: validationResult.error.flatten().fieldErrors 
      };
    }

    // 2. Call Service to Upload
    await projectService.uploadSow(projectId, formData);

    // 3. Revalidate Project Page to show "Processing" status
    revalidatePath(`/admin/sow-review/${projectId}`);
    revalidateTag(`project-${projectId}`);

    return { success: true, message: 'SOW uploaded successfully. Processing started.' };
  } catch (error: any) {
    console.error('SOW Upload Action Error:', error);
    return { success: false, message: error.message || 'Failed to upload SOW.' };
  }
}

/**
 * Updates the status of a project.
 */
export async function updateProjectStatusAction(
  projectId: string,
  status: string
): Promise<ProjectActionState> {
  try {
    await projectService.updateStatus(projectId, status);
    revalidatePath(`/admin/projects/${projectId}`);
    return { success: true, message: 'Project status updated.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to update status.' };
  }
}

/**
 * Approves the Project Brief, locking SOW data and triggering vendor matching.
 */
export async function approveProjectBriefAction(
  projectId: string
): Promise<ProjectActionState> {
  try {
    await projectService.approveBrief(projectId);
    revalidatePath(`/admin/sow-review/${projectId}`);
    return { success: true, message: 'Project Brief approved. Vendor matching initiated.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to approve brief.' };
  }
}

/**
 * Saves edits to the extracted SOW data (Human-in-the-loop).
 */
export async function saveSowDataAction(
  projectId: string,
  data: any
): Promise<ProjectActionState> {
  try {
    await projectService.updateSowData(projectId, data);
    revalidatePath(`/admin/sow-review/${projectId}`);
    return { success: true, message: 'SOW data saved successfully.' };
  } catch (error: any) {
    return { success: false, message: error.message || 'Failed to save SOW data.' };
  }
}