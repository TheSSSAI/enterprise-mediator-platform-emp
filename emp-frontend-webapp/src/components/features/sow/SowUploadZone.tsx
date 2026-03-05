'use client';

import React, { useRef, useState } from 'react';
import { useFileUpload } from '@/hooks/use-file-upload';
import { CloudArrowUpIcon, DocumentIcon, XCircleIcon } from '@heroicons/react/24/outline';

interface SowUploadZoneProps {
  projectId: string;
  onUploadComplete?: () => void;
}

/**
 * SowUploadZone Component
 * 
 * Handles drag-and-drop file selection and uploading for Statement of Work documents.
 * Validates file type and size before initiating upload via the custom hook.
 */
export function SowUploadZone({ projectId, onUploadComplete }: SowUploadZoneProps) {
  const { upload, isUploading, progress, error, reset } = useFileUpload({
    endpoint: `/projects/${projectId}/sow`,
    onSuccess: onUploadComplete
  });

  const [isDragActive, setIsDragActive] = useState(false);
  const fileInputRef = useRef<HTMLInputElement>(null);

  const handleDragEnter = (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragActive(true);
  };

  const handleDragLeave = (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragActive(false);
  };

  const handleDragOver = (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
  };

  const handleDrop = (e: React.DragEvent) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragActive(false);
    
    if (e.dataTransfer.files && e.dataTransfer.files.length > 0) {
      handleFiles(e.dataTransfer.files);
    }
  };

  const handleFileInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files.length > 0) {
      handleFiles(e.target.files);
    }
  };

  const handleFiles = (files: FileList) => {
    const file = files[0];
    
    // Client-side Validation (Level 4 Guard)
    const validTypes = ['application/pdf', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document'];
    const maxSize = 10 * 1024 * 1024; // 10MB

    if (!validTypes.includes(file.type)) {
      alert('Invalid file type. Please upload a PDF or DOCX file.');
      return;
    }

    if (file.size > maxSize) {
      alert('File size exceeds 10MB limit.');
      return;
    }

    const formData = new FormData();
    formData.append('file', file);
    upload(formData);
  };

  const triggerFileInput = () => {
    fileInputRef.current?.click();
  };

  if (isUploading) {
    return (
      <div className="w-full h-64 rounded-xl border-2 border-dashed border-blue-200 bg-blue-50 flex flex-col items-center justify-center p-6">
        <div className="w-full max-w-xs">
          <div className="flex justify-between text-sm font-medium text-slate-700 mb-2">
            <span>Uploading...</span>
            <span>{progress}%</span>
          </div>
          <div className="w-full bg-slate-200 rounded-full h-2.5">
            <div 
              className="bg-blue-600 h-2.5 rounded-full transition-all duration-300" 
              style={{ width: `${progress}%` }}
            ></div>
          </div>
          <p className="text-xs text-slate-500 mt-2 text-center">Processing document for AI analysis...</p>
        </div>
      </div>
    );
  }

  return (
    <div className="w-full">
      {error && (
        <div className="mb-4 p-4 rounded-lg bg-red-50 border border-red-200 flex items-start">
          <XCircleIcon className="h-5 w-5 text-red-500 mt-0.5 mr-2" />
          <div className="flex-1">
            <h4 className="text-sm font-semibold text-red-800">Upload Failed</h4>
            <p className="text-sm text-red-600 mt-1">{error.message}</p>
          </div>
          <button onClick={reset} className="text-xs font-medium text-red-700 hover:text-red-900">
            Dismiss
          </button>
        </div>
      )}

      <div
        onDragEnter={handleDragEnter}
        onDragLeave={handleDragLeave}
        onDragOver={handleDragOver}
        onDrop={handleDrop}
        onClick={triggerFileInput}
        className={`
          relative w-full h-64 rounded-xl border-2 border-dashed transition-all duration-200 cursor-pointer flex flex-col items-center justify-center p-6
          ${isDragActive 
            ? 'border-blue-500 bg-blue-50 scale-[1.01]' 
            : 'border-slate-300 bg-white hover:bg-slate-50 hover:border-slate-400'
          }
        `}
      >
        <input
          ref={fileInputRef}
          type="file"
          className="hidden"
          accept=".pdf,.docx"
          onChange={handleFileInputChange}
        />
        
        <div className="h-12 w-12 rounded-full bg-slate-100 flex items-center justify-center mb-4">
          <CloudArrowUpIcon className={`h-6 w-6 ${isDragActive ? 'text-blue-600' : 'text-slate-400'}`} />
        </div>
        
        <h3 className="text-sm font-semibold text-slate-900 mb-1">
          Upload Statement of Work
        </h3>
        <p className="text-xs text-slate-500 mb-4 text-center max-w-xs">
          Drag and drop your PDF or DOCX file here, or click to browse. Max size 10MB.
        </p>
        
        <div className="flex items-center space-x-2 text-xs text-slate-400 bg-slate-100 px-3 py-1.5 rounded-full">
          <DocumentIcon className="h-4 w-4" />
          <span>Supports AI Extraction</span>
        </div>
      </div>
    </div>
  );
}