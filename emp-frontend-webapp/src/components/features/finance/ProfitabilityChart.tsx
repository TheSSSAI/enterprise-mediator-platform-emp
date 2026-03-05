'use client';

import React from 'react';
import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer
} from 'recharts';
import { formatCurrency } from '@/lib/utils';

interface ProfitabilityData {
  month: string;
  revenue: number;
  costs: number;
  profit: number;
}

interface ProfitabilityChartProps {
  data: ProfitabilityData[];
  height?: number;
}

/**
 * ProfitabilityChart Component
 * 
 * Visualizes financial performance (Revenue vs Costs vs Profit) over time.
 * Uses Recharts for responsive, SVG-based rendering.
 */
export function ProfitabilityChart({ data, height = 300 }: ProfitabilityChartProps) {
  if (!data || data.length === 0) {
    return (
      <div className="flex h-full items-center justify-center rounded-lg border-2 border-dashed border-slate-200 bg-slate-50 p-12">
        <p className="text-sm text-slate-500">No financial data available for visualization.</p>
      </div>
    );
  }

  return (
    <div className="w-full bg-white rounded-lg p-4" style={{ height }}>
      <ResponsiveContainer width="100%" height="100%">
        <BarChart
          data={data}
          margin={{
            top: 20,
            right: 30,
            left: 20,
            bottom: 5,
          }}
        >
          <CartesianGrid strokeDasharray="3 3" stroke="#f1f5f9" />
          <XAxis 
            dataKey="month" 
            stroke="#64748b" 
            fontSize={12} 
            tickLine={false} 
            axisLine={false} 
          />
          <YAxis 
            stroke="#64748b" 
            fontSize={12} 
            tickLine={false} 
            axisLine={false}
            tickFormatter={(value) => `$${value / 1000}k`} 
          />
          <Tooltip 
            cursor={{ fill: '#f8fafc' }}
            contentStyle={{ borderRadius: '8px', border: 'none', boxShadow: '0 4px 6px -1px rgb(0 0 0 / 0.1)' }}
            formatter={(value: number) => [formatCurrency(value, 'USD'), '']}
          />
          <Legend wrapperStyle={{ paddingTop: '20px' }} />
          <Bar dataKey="revenue" name="Revenue" fill="#3b82f6" radius={[4, 4, 0, 0]} />
          <Bar dataKey="costs" name="Vendor Costs" fill="#ef4444" radius={[4, 4, 0, 0]} />
          <Bar dataKey="profit" name="Net Profit" fill="#22c55e" radius={[4, 4, 0, 0]} />
        </BarChart>
      </ResponsiveContainer>
    </div>
  );
}