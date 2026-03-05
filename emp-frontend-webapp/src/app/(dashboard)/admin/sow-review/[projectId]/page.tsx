import React from 'react';
import { notFound } from 'next/navigation';
import { ProjectService } from '@/services/project.service';
import SowReviewComposite from '@/components/features/sow/SowReviewComposite';
import { SowExtractionData } from '@/lib/types';

interface SowReviewPageProps {
  params: {
    projectId: string;
  };
}

/**
 * Server Component for SOW Review Page.
 * Fetches initial data and renders the interactive composite component.
 */
export default async function SowReviewPage({ params }: SowReviewPageProps) {
  const { projectId } = params;

  if (!projectId) {
    notFound();
  }

  try {
    // Parallel fetching for performance
    const [project, sowData] = await Promise.all([
      ProjectService.getProjectById(projectId),
      ProjectService.getSowExtractionData(projectId)
    ]);

    if (!project || !sowData) {
      throw new Error('Project or SOW data not found');
    }

    // Transform API response to UI model if necessary, passing mostly direct DTOs
    // Assuming sowData matches SowExtractionData interface required by the composite
    
    return (
      <div className="space-y-6">
        <div className="flex justify-between items-center">
          <div>
            <h1 className="text-2xl font-bold text-gray-900">Statement of Work Review</h1>
            <p className="mt-1 text-sm text-gray-500">
              Project: <span className="font-medium text-gray-900">{project.name}</span>
            </p>
          </div>
          <div className="flex items-center space-x-3">
            <span className={`inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium ${
              project.status === 'Pending' ? 'bg-yellow-100 text-yellow-800' : 'bg-green-100 text-green-800'
            }`}>
              {project.status}
            </span>
          </div>
        </div>

        <div className="bg-white shadow rounded-lg border border-gray-200 overflow-hidden">
          <SowReviewComposite 
            projectId={projectId} 
            initialData={sowData} 
          />
        </div>
      </div>
    );

  } catch (error) {
    // Log error internally
    console.error(`Failed to load SOW review for project ${projectId}:`, error);
    
    return (
      <div className="min-h-[400px] flex flex-col items-center justify-center text-center p-8 bg-white rounded-lg border border-gray-200">
        <div className="h-12 w-12 text-red-500 mb-4">
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor">
            <path strokeLinecap="round" strokeLinejoin="round" d="M12 9v3.75m9-.75a9 9 0 11-18 0 9 9 0 0118 0zm-9 3.75h.008v.008H12v-.008z" />
          </svg>
        </div>
        <h3 className="text-lg font-medium text-gray-900">Failed to load SOW Data</h3>
        <p className="mt-2 text-sm text-gray-500 max-w-sm">
          There was a problem retrieving the SOW extraction data. The document may still be processing or an error occurred.
        </p>
        <button 
          onClick={() => window.location.reload()} // Simple client-side reload
          className="mt-6 inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700"
        >
          Try Again
        </button>
      </div>
    );
  }
}