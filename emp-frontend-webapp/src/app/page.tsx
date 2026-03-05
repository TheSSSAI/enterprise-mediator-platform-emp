import { redirect } from 'next/navigation';

/**
 * Root Landing Page
 * 
 * In this enterprise B2B context, the root path acts as a gateway.
 * Unauthenticated users are directed to login.
 * Authenticated users (handled by middleware redirection logic) would typically go to dashboard.
 * 
 * We explicitly redirect to login here to enforce the entry point.
 */
export default function LandingPage() {
  redirect('/login');
}