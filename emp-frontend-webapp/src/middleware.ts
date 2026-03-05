import { NextResponse } from 'next/server';
import type { NextRequest } from 'next/server';
import { AUTH_COOKIE_NAME } from '@/lib/constants';

/**
 * Next.js Edge Middleware for Route Protection
 * 
 * Responsibilities:
 * 1. Validate session presence via HttpOnly cookies
 * 2. Protect /admin routes from unauthorized access
 * 3. Allow public access to /public, /login, and static assets
 */
export function middleware(request: NextRequest) {
  const { pathname } = request.nextUrl;

  // 1. Define Route Groups
  const isPublicRoute = 
    pathname.startsWith('/public') || 
    pathname === '/login' ||
    pathname === '/';
    
  const isStaticAsset = 
    pathname.startsWith('/_next') || 
    pathname.startsWith('/static') || 
    pathname.includes('.') || // Files like favicon.ico, robots.txt
    pathname.startsWith('/api/'); // API routes handle their own auth logic

  // 2. Bypass static assets and API routes
  if (isStaticAsset) {
    return NextResponse.next();
  }

  // 3. Check Authentication State
  const authToken = request.cookies.get(AUTH_COOKIE_NAME)?.value;
  const isAuthenticated = !!authToken;

  // 4. Handle Routing Logic
  
  // Scenario A: Unauthenticated User trying to access Protected Route
  if (!isPublicRoute && !isAuthenticated) {
    const loginUrl = new URL('/login', request.url);
    // Persist the original URL to redirect back after login
    loginUrl.searchParams.set('callbackUrl', pathname);
    return NextResponse.redirect(loginUrl);
  }

  // Scenario B: Authenticated User trying to access Login page
  if (pathname === '/login' && isAuthenticated) {
    // Redirect to default dashboard
    return NextResponse.redirect(new URL('/admin/dashboard', request.url));
  }

  // Scenario C: Authenticated User accessing Protected Route OR Public User accessing Public Route
  return NextResponse.next();
}

/**
 * Matcher Configuration
 * Excludes internal Next.js paths and static files to optimize performance.
 */
export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico (favicon file)
     */
    '/((?!api|_next/static|_next/image|favicon.ico).*)',
  ],
};