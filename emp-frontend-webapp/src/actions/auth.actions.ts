'use server';

import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';
import { z } from 'zod';
import { AuthService } from '@/services/auth.service';
import { LoginSchema, RegisterSchema } from '@/lib/schemas';
import { AuthTokens, UserProfile } from '@/lib/types';
import { AUTH_COOKIE_NAME, REFRESH_COOKIE_NAME, COOKIE_OPTIONS } from '@/lib/constants';

// Standardized Action Response Type
export type ActionState<T = null> = {
  success: boolean;
  data?: T;
  error?: string;
  fieldErrors?: Record<string, string[]>;
};

const authService = new AuthService();

/**
 * Handles user login with comprehensive validation and secure cookie management.
 */
export async function loginAction(
  prevState: ActionState<UserProfile> | null,
  formData: FormData
): Promise<ActionState<UserProfile>> {
  try {
    // 1. Extract and Validate Input
    const rawData = Object.fromEntries(formData.entries());
    const validatedFields = LoginSchema.safeParse(rawData);

    if (!validatedFields.success) {
      return {
        success: false,
        fieldErrors: validatedFields.error.flatten().fieldErrors,
        error: 'Invalid credentials format.',
      };
    }

    // 2. Authenticate via Service Layer
    const response = await authService.login(validatedFields.data);

    // 3. Securely Set Cookies
    const cookieStore = cookies();
    
    cookieStore.set(AUTH_COOKIE_NAME, response.tokens.accessToken, {
      ...COOKIE_OPTIONS,
      expires: new Date(Date.now() + response.tokens.expiresIn * 1000),
    });

    cookieStore.set(REFRESH_COOKIE_NAME, response.tokens.refreshToken, {
      ...COOKIE_OPTIONS,
      expires: new Date(Date.now() + 7 * 24 * 60 * 60 * 1000), // 7 days
    });

    // 4. Return Success (Redirect happens after return in UI or via redirect())
    // We return data here so UI can update state if needed before redirect
    return {
      success: true,
      data: response.user,
    };
  } catch (error: any) {
    console.error('Login Action Error:', error);
    return {
      success: false,
      error: error.message || 'Authentication failed. Please try again.',
    };
  }
}

/**
 * Handles user logout by clearing sessions and cookies.
 */
export async function logoutAction(): Promise<void> {
  const cookieStore = cookies();
  const accessToken = cookieStore.get(AUTH_COOKIE_NAME)?.value;

  if (accessToken) {
    try {
      await authService.logout();
    } catch (error) {
      console.warn('Logout service call failed, proceeding to clear cookies:', error);
    }
  }

  // Always clear cookies to ensure client-side logout
  cookieStore.delete(AUTH_COOKIE_NAME);
  cookieStore.delete(REFRESH_COOKIE_NAME);

  redirect('/login');
}

/**
 * Handles MFA Verification step.
 */
export async function verifyMfaAction(
  userId: string,
  code: string
): Promise<ActionState<UserProfile>> {
  try {
    if (!code || code.length !== 6) {
      return { success: false, error: 'Invalid verification code.' };
    }

    const response = await authService.verifyMfa(userId, code);

    const cookieStore = cookies();
    cookieStore.set(AUTH_COOKIE_NAME, response.tokens.accessToken, COOKIE_OPTIONS);
    cookieStore.set(REFRESH_COOKIE_NAME, response.tokens.refreshToken, COOKIE_OPTIONS);

    return { success: true, data: response.user };
  } catch (error: any) {
    return {
      success: false,
      error: error.message || 'MFA verification failed.',
    };
  }
}

/**
 * Registers a new user (Internal, Client, or Vendor).
 */
export async function registerAction(
  prevState: any,
  formData: FormData
): Promise<ActionState<void>> {
  try {
    const rawData = Object.fromEntries(formData.entries());
    const validatedFields = RegisterSchema.safeParse(rawData);

    if (!validatedFields.success) {
      return {
        success: false,
        fieldErrors: validatedFields.error.flatten().fieldErrors,
        error: 'Registration validation failed.',
      };
    }

    await authService.register(validatedFields.data);

    return { success: true };
  } catch (error: any) {
    return {
      success: false,
      error: error.message || 'Registration failed.',
    };
  }
}

/**
 * Helper to refresh tokens server-side if needed during other actions.
 */
export async function refreshSessionAction(): Promise<boolean> {
  const cookieStore = cookies();
  const refreshToken = cookieStore.get(REFRESH_COOKIE_NAME)?.value;

  if (!refreshToken) return false;

  try {
    const tokens = await authService.refreshToken(refreshToken);
    
    cookieStore.set(AUTH_COOKIE_NAME, tokens.accessToken, COOKIE_OPTIONS);
    if (tokens.refreshToken) {
      cookieStore.set(REFRESH_COOKIE_NAME, tokens.refreshToken, COOKIE_OPTIONS);
    }
    return true;
  } catch (error) {
    return false;
  }
}