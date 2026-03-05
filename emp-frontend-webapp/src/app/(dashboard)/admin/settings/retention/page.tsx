'use client';

import React, { useTransition } from 'react';
import { useForm } from 'react-hook-form';
import { updateRetentionPolicy } from '@/actions/finance.actions'; // Assuming finance/admin actions handle system config

interface RetentionFormInput {
  auditLogRetentionDays: number;
  financialRecordRetentionDays: number;
  projectDataRetentionDays: number;
}

export default function RetentionSettingsPage() {
  const [isPending, startTransition] = useTransition();
  const [saveStatus, setSaveStatus] = React.useState<'idle' | 'success' | 'error'>('idle');

  // Ideally, defaultValues would come from a server loader, but simplified here for client-first approach
  // In a real implementation, we'd pass initialData as a prop from a Server Page wrapper.
  const { register, handleSubmit, formState: { errors } } = useForm<RetentionFormInput>({
    defaultValues: {
      auditLogRetentionDays: 365,
      financialRecordRetentionDays: 2555, // 7 years
      projectDataRetentionDays: 1095, // 3 years
    }
  });

  const onSubmit = (data: RetentionFormInput) => {
    setSaveStatus('idle');
    startTransition(async () => {
      try {
        const result = await updateRetentionPolicy(data);
        if (result.success) {
          setSaveStatus('success');
        } else {
          setSaveStatus('error');
        }
      } catch (e) {
        setSaveStatus('error');
      }
    });
  };

  return (
    <div className="max-w-2xl mx-auto space-y-8">
      <div>
        <h1 className="text-2xl font-bold text-gray-900">Data Retention Policy</h1>
        <p className="mt-1 text-sm text-gray-500">
          Configure how long system data is retained before automated archival or deletion.
          Changes here are audited and may have legal compliance implications.
        </p>
      </div>

      <form onSubmit={handleSubmit(onSubmit)} className="space-y-6 bg-white p-6 rounded-lg shadow border border-gray-200">
        
        {saveStatus === 'success' && (
          <div className="bg-green-50 border border-green-200 text-green-700 px-4 py-3 rounded text-sm">
            Retention policies updated successfully.
          </div>
        )}
        
        {saveStatus === 'error' && (
          <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded text-sm">
            Failed to update policies. Please check your permissions and try again.
          </div>
        )}

        <div>
          <label className="block text-sm font-medium text-gray-700">
            Audit Logs (Days)
          </label>
          <div className="mt-1">
            <input
              type="number"
              {...register('auditLogRetentionDays', { required: true, min: 30 })}
              className="block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border"
            />
            <p className="mt-1 text-xs text-gray-500">Minimum 30 days recommended for security monitoring.</p>
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">
            Financial Records (Days)
          </label>
          <div className="mt-1">
            <input
              type="number"
              {...register('financialRecordRetentionDays', { required: true, min: 2555 })}
              className="block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border"
            />
            <p className="mt-1 text-xs text-gray-500">Regulatory minimum is often 7 years (2555 days).</p>
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-gray-700">
            Project Data (Days)
          </label>
          <div className="mt-1">
            <input
              type="number"
              {...register('projectDataRetentionDays', { required: true, min: 365 })}
              className="block w-full border-gray-300 rounded-md shadow-sm focus:ring-blue-500 focus:border-blue-500 sm:text-sm px-3 py-2 border"
            />
            <p className="mt-1 text-xs text-gray-500">Time after project completion before archiving.</p>
          </div>
        </div>

        <div className="pt-4 border-t border-gray-200 flex justify-end">
          <button
            type="submit"
            disabled={isPending}
            className="inline-flex justify-center py-2 px-4 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 disabled:opacity-50"
          >
            {isPending ? 'Saving...' : 'Save Changes'}
          </button>
        </div>
      </form>
    </div>
  );
}