import { apiClient } from '@/services/api-client';
import type { 
  AuthResponse, 
  LoginCredentials, 
  MfaVerificationRequest, 
  UserProfile, 
  PasswordResetRequest, 
  PasswordResetConfirmation,
  MfaSetupResponse,
  ApiResponse
} from '@/lib/types';

/**
 * Service responsible for handling authentication and authorization workflows.
 * Interacts with the Identity Provider via the API Gateway.
 */
class AuthService {
  private readonly baseUrl = '/auth';

  /**
   * Authenticates a user with email and password.
   * @param credentials The user's login credentials.
   * @returns The authentication response containing tokens or MFA challenge.
   */
  async login(credentials: LoginCredentials): Promise<AuthResponse> {
    return await apiClient.post<AuthResponse>(`${this.baseUrl}/login`, credentials);
  }

  /**
   * Logs out the current user by invalidating the session server-side.
   */
  async logout(): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/logout`, {});
  }

  /**
   * Verifies the Time-based One-Time Password (TOTP) for MFA.
   * @param data The MFA verification payload containing the code and session.
   * @returns The final authentication response with access tokens.
   */
  async verifyMfa(data: MfaVerificationRequest): Promise<AuthResponse> {
    return await apiClient.post<AuthResponse>(`${this.baseUrl}/mfa/verify`, data);
  }

  /**
   * Initiates the MFA setup process for the current user.
   * @returns The secret key and QR code URL for authenticator app setup.
   */
  async setupMfa(): Promise<MfaSetupResponse> {
    return await apiClient.post<MfaSetupResponse>(`${this.baseUrl}/mfa/setup`, {});
  }

  /**
   * Enables MFA for the user after successful verification of the first code.
   * @param code The 6-digit code from the authenticator app.
   */
  async enableMfa(code: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/mfa/enable`, { code });
  }

  /**
   * Refreshes the access token using the HttpOnly refresh token cookie.
   * Note: The cookie is handled automatically by the browser/server interaction.
   */
  async refreshToken(): Promise<AuthResponse> {
    return await apiClient.post<AuthResponse>(`${this.baseUrl}/refresh`, {});
  }

  /**
   * Requests a password reset email for the specified user.
   * @param data The request payload containing the email.
   */
  async requestPasswordReset(data: PasswordResetRequest): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/password/forgot`, data);
  }

  /**
   * Completes the password reset process with the token and new password.
   * @param data The confirmation payload.
   */
  async confirmPasswordReset(data: PasswordResetConfirmation): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/password/reset`, data);
  }

  /**
   * Retrieves the profile of the currently authenticated user.
   * @returns The user profile data.
   */
  async getCurrentUser(): Promise<UserProfile> {
    return await apiClient.get<UserProfile>(`${this.baseUrl}/me`, ['user-profile']);
  }
}

export const authService = new AuthService();