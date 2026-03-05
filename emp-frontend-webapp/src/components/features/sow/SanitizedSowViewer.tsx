'use client';

import React from 'react';

interface SanitizedSowViewerProps {
  content: string;
  isLoading?: boolean;
}

/**
 * SanitizedSowViewer Component
 * 
 * Displays the PII-redacted text extracted from the SOW document.
 * Used for visual verification against the extracted structured data.
 */
export function SanitizedSowViewer({ content, isLoading = false }: SanitizedSowViewerProps) {
  if (isLoading) {
    return (
      <div className="h-full w-full flex items-center justify-center p-12 bg-slate-50 border border-slate-200 rounded-lg animate-pulse">
        <div className="space-y-4 w-full max-w-md">
          <div className="h-4 bg-slate-200 rounded w-3/4"></div>
          <div className="h-4 bg-slate-200 rounded"></div>
          <div className="h-4 bg-slate-200 rounded"></div>
          <div className="h-4 bg-slate-200 rounded w-5/6"></div>
        </div>
      </div>
    );
  }

  if (!content) {
    return (
      <div className="h-full w-full flex items-center justify-center p-12 bg-slate-50 border border-slate-200 rounded-lg text-slate-400 text-sm">
        No sanitized content available. Upload an SOW to begin.
      </div>
    );
  }

  return (
    <div className="h-full flex flex-col bg-white rounded-lg shadow-sm border border-slate-200 overflow-hidden">
      <div className="bg-slate-50 px-4 py-3 border-b border-slate-200 flex justify-between items-center">
        <h3 className="text-sm font-semibold text-slate-700">Source Document (Sanitized)</h3>
        <span className="inline-flex items-center rounded-full bg-green-100 px-2 py-0.5 text-xs font-medium text-green-800">
          PII Removed
        </span>
      </div>
      <div className="flex-1 overflow-auto p-6">
        <article className="prose prose-sm prose-slate max-w-none">
          {/* Simple whitespace preserving rendering for plain text content */}
          <div className="whitespace-pre-wrap font-mono text-xs leading-relaxed text-slate-600">
            {content}
          </div>
        </article>
      </div>
    </div>
  );
}