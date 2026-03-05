import React from 'react';
import { notFound } from 'next/navigation';
import { ProjectService } from '@/services/project.service';
import SanitizedSowViewer from '@/components/features/sow/SanitizedSowViewer';

interface ProjectBriefPageProps {
  params: {
    token: string;
  };
}

export default async function ProjectBriefPage({ params }: ProjectBriefPageProps) {
  const { token } = params;

  if (!token) notFound();

  try {
    const briefData = await ProjectService.getProjectBriefByToken(token);

    if (!briefData) {
      return (
        <div className="max-w-lg mx-auto bg-white p-8 rounded-lg shadow text-center mt-10">
          <h2 className="text-xl font-semibold text-gray-800">Brief Not Found</h2>
          <p className="text-gray-600 mt-2">The project brief link is invalid or has expired.</p>
        </div>
      );
    }

    return (
      <div className="max-w-4xl mx-auto space-y-8">
        <div className="bg-white shadow overflow-hidden sm:rounded-lg">
          <div className="px-4 py-5 sm:px-6 border-b border-gray-200">
            <h3 className="text-lg leading-6 font-medium text-gray-900">Project Opportunity</h3>
            <p className="mt-1 max-w-2xl text-sm text-gray-500">
              Review the sanitized project scope and requirements below.
            </p>
          </div>
          <div className="px-4 py-5 sm:p-6">
            <SanitizedSowViewer 
              data={briefData.sowData} 
              readOnly={true}
            />
          </div>
          <div className="px-4 py-4 sm:px-6 bg-gray-50 border-t border-gray-200 flex justify-end space-x-3">
            <button className="inline-flex items-center px-4 py-2 border border-gray-300 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
              Decline
            </button>
            <button className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500">
              Submit Proposal
            </button>
          </div>
        </div>
      </div>
    );
  } catch (error) {
    return (
      <div className="max-w-lg mx-auto bg-white p-8 rounded-lg shadow text-center mt-10">
        <h2 className="text-xl font-semibold text-red-600">Service Error</h2>
        <p className="text-gray-600 mt-2">Unable to load project brief. Please try again later.</p>
      </div>
    );
  }
}