import React from 'react';
import { AuditService } from '@/services/audit.service';
import { AuditLogEntry } from '@/types/audit';

export const dynamic = 'force-dynamic'; // Audit logs are real-time data

export default async function AuditTrailPage() {
  let logs: AuditLogEntry[] = [];
  let error: string | null = null;

  try {
    logs = await AuditService.getAuditLogs({ limit: 50, offset: 0 });
  } catch (e) {
    error = 'Failed to fetch audit logs. Please try again later.';
  }

  return (
    <div className="space-y-6">
      <div className="sm:flex sm:items-center">
        <div className="sm:flex-auto">
          <h1 className="text-2xl font-bold text-gray-900">System Audit Trail</h1>
          <p className="mt-2 text-sm text-gray-700">
            A comprehensive immutable log of all system activities, security events, and data changes.
          </p>
        </div>
        <div className="mt-4 sm:mt-0 sm:ml-16 sm:flex-none">
          <button className="inline-flex items-center justify-center rounded-md border border-gray-300 bg-white px-4 py-2 text-sm font-medium text-gray-700 shadow-sm hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-offset-2 sm:w-auto">
            Export CSV
          </button>
        </div>
      </div>

      {error ? (
        <div className="bg-red-50 p-4 rounded-md border border-red-200">
          <div className="flex">
            <div className="flex-shrink-0">
              <svg className="h-5 w-5 text-red-400" viewBox="0 0 20 20" fill="currentColor">
                <path fillRule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clipRule="evenodd" />
              </svg>
            </div>
            <div className="ml-3">
              <h3 className="text-sm font-medium text-red-800">Error loading data</h3>
              <div className="mt-2 text-sm text-red-700">{error}</div>
            </div>
          </div>
        </div>
      ) : (
        <div className="mt-8 flex flex-col">
          <div className="-my-2 -mx-4 overflow-x-auto sm:-mx-6 lg:-mx-8">
            <div className="inline-block min-w-full py-2 align-middle md:px-6 lg:px-8">
              <div className="overflow-hidden shadow ring-1 ring-black ring-opacity-5 md:rounded-lg">
                <table className="min-w-full divide-y divide-gray-300">
                  <thead className="bg-gray-50">
                    <tr>
                      <th scope="col" className="px-3 py-3.5 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Timestamp</th>
                      <th scope="col" className="px-3 py-3.5 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">User</th>
                      <th scope="col" className="px-3 py-3.5 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Action</th>
                      <th scope="col" className="px-3 py-3.5 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Entity</th>
                      <th scope="col" className="px-3 py-3.5 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">Details</th>
                    </tr>
                  </thead>
                  <tbody className="divide-y divide-gray-200 bg-white">
                    {logs.map((log) => (
                      <tr key={log.id}>
                        <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
                          {new Date(log.timestamp).toLocaleString()}
                        </td>
                        <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-900 font-medium">
                          {log.userName || log.userId}
                        </td>
                        <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
                          <span className="inline-flex items-center px-2 py-0.5 rounded text-xs font-medium bg-gray-100 text-gray-800">
                            {log.actionType}
                          </span>
                        </td>
                        <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
                          {log.entityType} ({log.entityId})
                        </td>
                        <td className="px-3 py-4 text-sm text-gray-500 max-w-xs truncate">
                          {JSON.stringify(log.changes || log.metadata)}
                        </td>
                      </tr>
                    ))}
                    {logs.length === 0 && (
                      <tr>
                        <td colSpan={5} className="px-3 py-8 text-center text-sm text-gray-500">
                          No audit records found.
                        </td>
                      </tr>
                    )}
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}