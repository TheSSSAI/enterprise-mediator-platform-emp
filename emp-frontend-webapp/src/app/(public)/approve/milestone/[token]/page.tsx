import React from 'react';
import { notFound } from 'next/navigation';
import { ProjectService } from '@/services/project.service';
import { approveMilestone } from '@/actions/project.actions';

interface MilestoneApprovalPageProps {
  params: {
    token: string;
  };
}

export default async function MilestoneApprovalPage({ params }: MilestoneApprovalPageProps) {
  const { token } = params;

  if (!token) notFound();

  let milestoneData = null;
  let errorState = null;

  try {
    milestoneData = await ProjectService.getMilestoneByToken(token);
  } catch (error) {
    errorState = 'Invalid or expired approval token.';
  }

  if (!milestoneData && !errorState) {
    errorState = 'Milestone data not found.';
  }

  // Handle Server Action submission from within the component
  async function handleApprove(formData: FormData) {
    'use server';
    await approveMilestone(token);
    // In a real app, we'd redirect to a success page or revalidate
  }

  if (errorState) {
    return (
      <div className="max-w-md mx-auto bg-white p-8 rounded-lg shadow text-center mt-10">
        <h2 className="text-lg font-bold text-red-600 mb-2">Access Denied</h2>
        <p className="text-gray-600">{errorState}</p>
      </div>
    );
  }

  return (
    <div className="max-w-2xl mx-auto bg-white shadow sm:rounded-lg overflow-hidden mt-10">
      <div className="px-4 py-5 sm:px-6 border-b border-gray-200 bg-gray-50">
        <h3 className="text-lg leading-6 font-medium text-gray-900">Milestone Approval Required</h3>
        <p className="mt-1 text-sm text-gray-500">
          Please review the deliverable details below and confirm approval.
        </p>
      </div>
      
      <div className="px-4 py-5 sm:p-6 space-y-4">
        <div>
          <h4 className="text-sm font-medium text-gray-500">Project</h4>
          <p className="mt-1 text-lg font-semibold text-gray-900">{milestoneData.projectName}</p>
        </div>
        
        <div className="border-t border-gray-100 pt-4">
          <h4 className="text-sm font-medium text-gray-500">Milestone</h4>
          <p className="mt-1 text-base text-gray-900">{milestoneData.name}</p>
        </div>

        <div className="border-t border-gray-100 pt-4">
          <h4 className="text-sm font-medium text-gray-500">Description</h4>
          <p className="mt-1 text-sm text-gray-700">{milestoneData.description}</p>
        </div>

        <div className="border-t border-gray-100 pt-4">
          <h4 className="text-sm font-medium text-gray-500">Amount to Release</h4>
          <p className="mt-1 text-2xl font-bold text-green-600">
            {new Intl.NumberFormat('en-US', { style: 'currency', currency: milestoneData.currency }).format(milestoneData.amount)}
          </p>
        </div>
      </div>

      <div className="px-4 py-4 sm:px-6 bg-gray-50 border-t border-gray-200 flex justify-end space-x-4">
        <form action={async () => {
          'use server';
          // Reject logic would go here
        }}>
          <button 
            type="submit"
            className="inline-flex items-center px-4 py-2 border border-gray-300 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-red-500"
          >
            Request Changes
          </button>
        </form>
        
        <form action={handleApprove}>
          <button 
            type="submit"
            className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-green-600 hover:bg-green-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-green-500"
          >
            Approve & Release Funds
          </button>
        </form>
      </div>
    </div>
  );
}