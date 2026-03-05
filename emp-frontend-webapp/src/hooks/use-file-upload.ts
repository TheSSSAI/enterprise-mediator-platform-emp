'use client';

import { useState, useCallback, ChangeEvent } from 'react';
import { uploadSowAction } from '@/actions/project.actions';
import { useUiStore } from '@/store/use-ui-store';

// Configuration constants based on requirements
const MAX_FILE_SIZE_MB = 10;
const MAX_FILE_SIZE_BYTES = MAX_FILE_SIZE_MB * 1024 * 1024;
const ALLOWED_TYPES = [
  'application/pdf',
  'application/vnd.openxmlformats-officedocument.wordprocessingml.document', // .docx
  'application/msword' // .doc
];

interface UseFileUploadReturn {
  file: File | null;
  previewUrl: string | null;
  isUploading: boolean;
  progress: number;
  error: string | null;
  handleFileSelect: (e: ChangeEvent<HTMLInputElement> | File) => void;
  handleUpload: (projectId: string) => Promise<boolean>;
  reset: () => void;
}

/**
 * Hook to manage file selection, validation, and upload state.
 * Bridges the UI component with the Server Action.
 */
export function useFileUpload(): UseFileUploadReturn {
  const [file, setFile] = useState<File | null>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);
  const [isUploading, setIsUploading] = useState(false);
  const [progress, setProgress] = useState(0);
  const [error, setError] = useState<string | null>(null);
  
  const { showToast } = useUiStore();

  const validateFile = (selectedFile: File): boolean => {
    // Size Check
    if (selectedFile.size > MAX_FILE_SIZE_BYTES) {
      setError(`File size exceeds ${MAX_FILE_SIZE_MB}MB limit.`);
      return false;
    }

    // Type Check
    if (!ALLOWED_TYPES.includes(selectedFile.type)) {
      setError('Invalid file type. Please upload a PDF or DOCX file.');
      return false;
    }

    return true;
  };

  const handleFileSelect = useCallback((input: ChangeEvent<HTMLInputElement> | File) => {
    setError(null);
    let selectedFile: File | null = null;

    if (input instanceof File) {
      selectedFile = input;
    } else if (input.target.files && input.target.files.length > 0) {
      selectedFile = input.target.files[0];
    }

    if (selectedFile) {
      if (validateFile(selectedFile)) {
        setFile(selectedFile);
        // Create object URL for preview if needed (mostly for images, but useful ref)
        const url = URL.createObjectURL(selectedFile);
        setPreviewUrl(url);
      } else {
        setFile(null);
        setPreviewUrl(null);
      }
    }
  }, []);

  const handleUpload = async (projectId: string): Promise<boolean> => {
    if (!file) {
      setError('No file selected.');
      return false;
    }

    setIsUploading(true);
    setProgress(10); // Start progress

    try {
      const formData = new FormData();
      formData.append('file', file);

      // Simulate progress since Server Actions don't stream upload progress back easily
      const progressInterval = setInterval(() => {
        setProgress((prev) => Math.min(prev + 10, 90));
      }, 200);

      const result = await uploadSowAction(projectId, formData);

      clearInterval(progressInterval);
      
      if (result.success) {
        setProgress(100);
        showToast({
          title: 'Upload Successful',
          message: 'SOW document has been uploaded and processing has started.',
          type: 'success',
        });
        return true;
      } else {
        setError(result.message || 'Upload failed.');
        setProgress(0);
        showToast({
          title: 'Upload Failed',
          message: result.message || 'An unexpected error occurred.',
          type: 'error',
        });
        return false;
      }
    } catch (err: any) {
      setError(err.message || 'Network error during upload.');
      setProgress(0);
      return false;
    } finally {
      setIsUploading(false);
    }
  };

  const reset = useCallback(() => {
    setFile(null);
    if (previewUrl) URL.revokeObjectURL(previewUrl);
    setPreviewUrl(null);
    setError(null);
    setProgress(0);
    setIsUploading(false);
  }, [previewUrl]);

  return {
    file,
    previewUrl,
    isUploading,
    progress,
    error,
    handleFileSelect,
    handleUpload,
    reset,
  };
}