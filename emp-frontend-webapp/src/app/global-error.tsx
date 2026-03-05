'use client';

import React from 'react';

export default function GlobalError({
  error,
  reset,
}: {
  error: Error & { digest?: string };
  reset: () => void;
}) {
  return (
    <html lang="en">
      <body className="bg-gray-50 flex items-center justify-center min-h-screen font-sans">
        <div className="w-full max-w-md p-8 bg-white rounded-lg shadow-lg border border-gray-200 text-center">
          <h2 className="text-2xl font-bold text-gray-900 mb-4">Critical System Error</h2>
          <p className="text-gray-600 mb-6">
            A critical error occurred within the application shell. Our team has been notified.
          </p>
          {error.digest && (
            <p className="text-xs text-gray-400 mb-6 font-mono bg-gray-100 p-2 rounded">
              Error ID: {error.digest}
            </p>
          )}
          <button
            onClick={() => reset()}
            className="w-full px-4 py-2 text-sm font-medium text-white bg-blue-600 rounded-md hover:bg-blue-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-blue-500 transition-colors"
          >
            Reload Application
          </button>
        </div>
      </body>
    </html>
  );
}