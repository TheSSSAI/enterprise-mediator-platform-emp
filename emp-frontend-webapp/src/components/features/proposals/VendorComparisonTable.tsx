'use client';

import React from 'react';
import { formatCurrency } from '@/lib/utils';
import { CheckCircleIcon } from '@heroicons/react/24/solid';

interface ComparisonData {
  vendorId: string;
  vendorName: string;
  totalCost: number;
  timelineWeeks: number;
  score: number; // AI Similarity Score
  skillsMatch: number; // Percentage
  status: string;
}

interface VendorComparisonTableProps {
  proposals: ComparisonData[];
  onSelectWinner: (vendorId: string) => void;
}

/**
 * VendorComparisonTable Component
 * 
 * Matrix view for side-by-side comparison of vendor proposals.
 * Highlights the best values in each category to aid decision making.
 */
export function VendorComparisonTable({ proposals, onSelectWinner }: VendorComparisonTableProps) {
  // Determine best metrics for highlighting
  const minCost = Math.min(...proposals.map(p => p.totalCost));
  const minTime = Math.min(...proposals.map(p => p.timelineWeeks));
  const maxScore = Math.max(...proposals.map(p => p.score));

  return (
    <div className="overflow-x-auto rounded-lg border border-slate-200 shadow-sm">
      <table className="min-w-full divide-y divide-slate-200">
        <thead className="bg-slate-50">
          <tr>
            <th scope="col" className="px-6 py-4 text-left text-xs font-semibold text-slate-500 uppercase tracking-wider bg-slate-50 sticky left-0 z-10 w-48">
              Criteria
            </th>
            {proposals.map((proposal) => (
              <th key={proposal.vendorId} scope="col" className="px-6 py-4 text-center text-sm font-bold text-slate-900 min-w-[200px]">
                {proposal.vendorName}
                {proposal.status === 'Shortlisted' && (
                  <span className="block mt-1 text-[10px] font-normal text-blue-600 bg-blue-50 py-0.5 rounded-full px-2 w-fit mx-auto">
                    Shortlisted
                  </span>
                )}
              </th>
            ))}
          </tr>
        </thead>
        <tbody className="bg-white divide-y divide-slate-200">
          
          {/* Total Cost Row */}
          <tr>
            <td className="px-6 py-4 text-sm font-medium text-slate-900 bg-slate-50 sticky left-0">
              Total Cost
            </td>
            {proposals.map((p) => (
              <td key={p.vendorId} className={`px-6 py-4 text-center text-sm ${p.totalCost === minCost ? 'bg-green-50 font-semibold text-green-700' : 'text-slate-600'}`}>
                {formatCurrency(p.totalCost, 'USD')}
                {p.totalCost === minCost && <span className="block text-[10px] text-green-600">Best Price</span>}
              </td>
            ))}
          </tr>

          {/* Timeline Row */}
          <tr>
            <td className="px-6 py-4 text-sm font-medium text-slate-900 bg-slate-50 sticky left-0">
              Timeline
            </td>
            {proposals.map((p) => (
              <td key={p.vendorId} className={`px-6 py-4 text-center text-sm ${p.timelineWeeks === minTime ? 'bg-green-50 font-semibold text-green-700' : 'text-slate-600'}`}>
                {p.timelineWeeks} Weeks
                {p.timelineWeeks === minTime && <span className="block text-[10px] text-green-600">Fastest</span>}
              </td>
            ))}
          </tr>

          {/* AI Match Score Row */}
          <tr>
            <td className="px-6 py-4 text-sm font-medium text-slate-900 bg-slate-50 sticky left-0">
              AI Match Score
            </td>
            {proposals.map((p) => (
              <td key={p.vendorId} className={`px-6 py-4 text-center text-sm ${p.score === maxScore ? 'bg-blue-50 font-semibold text-blue-700' : 'text-slate-600'}`}>
                {p.score}%
                {p.score === maxScore && <span className="block text-[10px] text-blue-600">Highest Match</span>}
              </td>
            ))}
          </tr>

          {/* Skills Match Row */}
          <tr>
            <td className="px-6 py-4 text-sm font-medium text-slate-900 bg-slate-50 sticky left-0">
              Skills Coverage
            </td>
            {proposals.map((p) => (
              <td key={p.vendorId} className="px-6 py-4 text-center text-sm text-slate-600">
                <div className="w-full bg-slate-200 rounded-full h-2.5 mb-1">
                  <div className="bg-slate-600 h-2.5 rounded-full" style={{ width: `${p.skillsMatch}%` }}></div>
                </div>
                <span className="text-xs">{p.skillsMatch}%</span>
              </td>
            ))}
          </tr>

          {/* Action Row */}
          <tr>
            <td className="px-6 py-4 text-sm font-medium text-slate-900 bg-slate-50 sticky left-0">
              Action
            </td>
            {proposals.map((p) => (
              <td key={p.vendorId} className="px-6 py-4 text-center">
                <button
                  onClick={() => onSelectWinner(p.vendorId)}
                  className="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md shadow-sm text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500"
                >
                  <CheckCircleIcon className="h-4 w-4 mr-2" />
                  Select Winner
                </button>
              </td>
            ))}
          </tr>
        </tbody>
      </table>
    </div>
  );
}