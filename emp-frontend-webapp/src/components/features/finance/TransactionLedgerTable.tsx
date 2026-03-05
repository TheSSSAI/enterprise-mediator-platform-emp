'use client';

import React, { useState } from 'react';
import type { TransactionDTO } from '@/lib/types';
import { formatCurrency, formatDate } from '@/lib/utils';
import { 
  ChevronLeftIcon, 
  ChevronRightIcon, 
  FunnelIcon, 
  ArrowDownTrayIcon 
} from '@heroicons/react/24/outline';

interface TransactionLedgerTableProps {
  transactions: TransactionDTO[];
  totalCount: number;
  currentPage: number;
  onPageChange: (page: number) => void;
  onExport: () => void;
}

/**
 * TransactionLedgerTable Component
 * 
 * Displays a paginated list of financial transactions.
 * Features status badges, currency formatting, and basic pagination controls.
 */
export function TransactionLedgerTable({ 
  transactions, 
  totalCount, 
  currentPage, 
  onPageChange,
  onExport 
}: TransactionLedgerTableProps) {
  const [filterType, setFilterType] = useState<'ALL' | 'PAYOUT' | 'REFUND' | 'INVOICE'>('ALL');

  const getStatusBadge = (status: string) => {
    const styles: Record<string, string> = {
      COMPLETED: 'bg-green-100 text-green-800',
      PENDING: 'bg-yellow-100 text-yellow-800',
      FAILED: 'bg-red-100 text-red-800',
      PROCESSING: 'bg-blue-100 text-blue-800'
    };
    return (
      <span className={`inline-flex rounded-full px-2 text-xs font-semibold leading-5 ${styles[status] || 'bg-gray-100 text-gray-800'}`}>
        {status}
      </span>
    );
  };

  const filteredTransactions = filterType === 'ALL' 
    ? transactions 
    : transactions.filter(t => t.type === filterType);

  return (
    <div className="bg-white shadow rounded-lg border border-slate-200 overflow-hidden">
      {/* Toolbar */}
      <div className="px-4 py-3 border-b border-slate-200 flex flex-col sm:flex-row justify-between items-center gap-4 bg-slate-50">
        <h3 className="text-base font-semibold leading-6 text-slate-900">Transaction Ledger</h3>
        <div className="flex items-center gap-2">
          <div className="relative">
            <select
              value={filterType}
              onChange={(e) => setFilterType(e.target.value as any)}
              className="block w-full rounded-md border-0 py-1.5 pl-3 pr-10 text-slate-900 ring-1 ring-inset ring-slate-300 focus:ring-2 focus:ring-blue-600 sm:text-sm sm:leading-6 appearance-none bg-white"
            >
              <option value="ALL">All Transactions</option>
              <option value="INVOICE">Invoices</option>
              <option value="PAYOUT">Payouts</option>
              <option value="REFUND">Refunds</option>
            </select>
            <div className="pointer-events-none absolute inset-y-0 right-0 flex items-center px-2 text-slate-500">
              <FunnelIcon className="h-4 w-4" />
            </div>
          </div>
          <button
            onClick={onExport}
            className="inline-flex items-center gap-x-1.5 rounded-md bg-white px-3 py-2 text-sm font-semibold text-slate-900 shadow-sm ring-1 ring-inset ring-slate-300 hover:bg-slate-50"
          >
            <ArrowDownTrayIcon className="-ml-0.5 h-5 w-5 text-slate-400" aria-hidden="true" />
            Export
          </button>
        </div>
      </div>

      {/* Table */}
      <div className="overflow-x-auto">
        <table className="min-w-full divide-y divide-slate-300">
          <thead className="bg-slate-50">
            <tr>
              <th scope="col" className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-slate-900 sm:pl-6">ID</th>
              <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-slate-900">Date</th>
              <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-slate-900">Description</th>
              <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-slate-900">Type</th>
              <th scope="col" className="px-3 py-3.5 text-right text-sm font-semibold text-slate-900">Amount</th>
              <th scope="col" className="px-3 py-3.5 text-center text-sm font-semibold text-slate-900">Status</th>
            </tr>
          </thead>
          <tbody className="divide-y divide-slate-200 bg-white">
            {filteredTransactions.length > 0 ? (
              filteredTransactions.map((transaction) => (
                <tr key={transaction.id} className="hover:bg-slate-50">
                  <td className="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-slate-900 sm:pl-6 font-mono">
                    {transaction.id.substring(0, 8)}...
                  </td>
                  <td className="whitespace-nowrap px-3 py-4 text-sm text-slate-500">
                    {formatDate(transaction.date)}
                  </td>
                  <td className="px-3 py-4 text-sm text-slate-500 max-w-xs truncate">
                    {transaction.description}
                  </td>
                  <td className="whitespace-nowrap px-3 py-4 text-sm text-slate-500">
                    {transaction.type}
                  </td>
                  <td className={`whitespace-nowrap px-3 py-4 text-sm text-right font-medium ${transaction.amount < 0 ? 'text-red-600' : 'text-slate-900'}`}>
                    {formatCurrency(transaction.amount, transaction.currency)}
                  </td>
                  <td className="whitespace-nowrap px-3 py-4 text-sm text-center">
                    {getStatusBadge(transaction.status)}
                  </td>
                </tr>
              ))
            ) : (
              <tr>
                <td colSpan={6} className="px-3 py-12 text-center text-sm text-slate-500">
                  No transactions found matching criteria.
                </td>
              </tr>
            )}
          </tbody>
        </table>
      </div>

      {/* Pagination */}
      <div className="flex items-center justify-between border-t border-slate-200 bg-white px-4 py-3 sm:px-6">
        <div className="hidden sm:flex sm:flex-1 sm:items-center sm:justify-between">
          <div>
            <p className="text-sm text-slate-700">
              Showing page <span className="font-medium">{currentPage}</span> of <span className="font-medium">{Math.ceil(totalCount / 10)}</span>
            </p>
          </div>
          <div>
            <nav className="isolate inline-flex -space-x-px rounded-md shadow-sm" aria-label="Pagination">
              <button
                onClick={() => onPageChange(Math.max(1, currentPage - 1))}
                disabled={currentPage === 1}
                className="relative inline-flex items-center rounded-l-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0 disabled:opacity-50"
              >
                <span className="sr-only">Previous</span>
                <ChevronLeftIcon className="h-5 w-5" aria-hidden="true" />
              </button>
              <button
                onClick={() => onPageChange(currentPage + 1)}
                disabled={currentPage * 10 >= totalCount}
                className="relative inline-flex items-center rounded-r-md px-2 py-2 text-gray-400 ring-1 ring-inset ring-gray-300 hover:bg-gray-50 focus:z-20 focus:outline-offset-0 disabled:opacity-50"
              >
                <span className="sr-only">Next</span>
                <ChevronRightIcon className="h-5 w-5" aria-hidden="true" />
              </button>
            </nav>
          </div>
        </div>
      </div>
    </div>
  );
}