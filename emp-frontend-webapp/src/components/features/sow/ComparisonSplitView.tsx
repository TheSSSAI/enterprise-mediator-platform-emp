'use client';

import React, { ReactNode } from 'react';

interface ComparisonSplitViewProps {
  leftPanel: ReactNode;
  rightPanel: ReactNode;
  className?: string;
}

/**
 * ComparisonSplitView Layout Component
 * 
 * Provides a responsive split-pane layout for side-by-side comparison
 * of the source SOW document and the extracted data form.
 * Collapses to vertical stack on mobile.
 */
export function ComparisonSplitView({ leftPanel, rightPanel, className = '' }: ComparisonSplitViewProps) {
  return (
    <div className={`grid grid-cols-1 lg:grid-cols-2 gap-6 h-full ${className}`}>
      {/* Left Panel: Source Document (Sticky on Desktop) */}
      <div className="h-[500px] lg:h-[calc(100vh-12rem)] lg:sticky lg:top-6 overflow-hidden">
        {leftPanel}
      </div>

      {/* Right Panel: Extraction Form (Scrollable) */}
      <div className="h-auto">
        {rightPanel}
      </div>
    </div>
  );
}