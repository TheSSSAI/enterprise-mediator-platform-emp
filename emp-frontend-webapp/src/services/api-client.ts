import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';

/**
 * Standard API Response structure expected from the Gateway
 */
export interface ApiResponse<T = any> {
  data: T;
  message?: string;
  success: boolean;
  errors?: Record<string, string[]>;
}

/**
 * Configuration options for Next.js specific fetch behaviors
 */
export interface RequestConfig extends RequestInit {
  skipAuth?: boolean;
  params?: Record<string, string | number | boolean | undefined>;
}

/**
 * Custom Error class for API related failures
 */
export class ApiError extends Error {
  public status: number;
  public data: any;
  public errors?: Record<string, string[]>;

  constructor(message: string, status: number, data?: any, errors?: Record<string, string[]>) {
    super(message);
    this.name = 'ApiError';
    this.status = status;
    this.data = data;
    this.errors = errors;
  }
}

/**
 * Core API Client for Server-Side communication (Server Actions/RSC)
 * Implements the BFF (Backend for Frontend) pattern connector.
 */
export class ApiClient {
  // Prefer internal Docker network URL if available, otherwise public
  private static baseUrl = process.env.API_URL || process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5000/api/v1';

  /**
   * Retrieves headers including Authentication token from HttpOnly cookies
   */
  private static async getHeaders(config?: RequestConfig): Promise<Headers> {
    const headers = new Headers(config?.headers);
    
    // Authorization
    if (!config?.skipAuth) {
      const cookieStore = cookies();
      const token = cookieStore.get('access_token')?.value;
      if (token) {
        headers.set('Authorization', `Bearer ${token}`);
      }
    }

    // Content-Type
    // Note: Do not set Content-Type for FormData, browser/runtime sets boundary automatically
    if (!headers.has('Content-Type') && !(config?.body instanceof FormData)) {
      headers.set('Content-Type', 'application/json');
    }

    // Accept
    if (!headers.has('Accept')) {
      headers.set('Accept', 'application/json');
    }

    return headers;
  }

  /**
   * Constructs the full URL with query parameters
   */
  private static buildUrl(endpoint: string, params?: Record<string, any>): string {
    // Remove leading slash to ensure clean join with base URL
    const cleanEndpoint = endpoint.startsWith('/') ? endpoint.substring(1) : endpoint;
    const url = new URL(`${this.baseUrl}/${cleanEndpoint}`);

    if (params) {
      Object.entries(params).forEach(([key, value]) => {
        if (value !== undefined && value !== null) {
          url.searchParams.append(key, String(value));
        }
      });
    }

    return url.toString();
  }

  /**
   * Generic handler for Fetch responses
   */
  private static async handleResponse<T>(response: Response): Promise<T> {
    // Handle 401 Unauthorized globally
    if (response.status === 401) {
      // In a Server Action, redirect throws a special error type to handle navigation
      // We let it bubble up or handle it based on context
      // For now, we assume the caller handles the redirect or we perform it here
      //redirect('/login'); // Uncomment if aggressive redirect is desired
    }

    const contentType = response.headers.get('content-type');
    const isJson = contentType?.includes('application/json');
    
    let data: any;
    
    try {
      if (isJson) {
        data = await response.json();
      } else {
        data = await response.text();
      }
    } catch (error) {
      // Failed to parse body
      data = null;
    }

    if (!response.ok) {
      const errorMessage = data?.message || data?.title || `API Error: ${response.statusText}`;
      const validationErrors = data?.errors;
      throw new ApiError(errorMessage, response.status, data, validationErrors);
    }

    return data as T;
  }

  /**
   * GET Request
   */
  public static async get<T>(endpoint: string, config?: RequestConfig): Promise<T> {
    const url = this.buildUrl(endpoint, config?.params);
    const headers = await this.getHeaders(config);

    const response = await fetch(url, {
      ...config,
      method: 'GET',
      headers,
    });

    return this.handleResponse<T>(response);
  }

  /**
   * POST Request
   */
  public static async post<T>(endpoint: string, body: any, config?: RequestConfig): Promise<T> {
    const url = this.buildUrl(endpoint, config?.params);
    const headers = await this.getHeaders(config);

    const isFormData = body instanceof FormData;
    const payload = isFormData ? body : JSON.stringify(body);

    const response = await fetch(url, {
      ...config,
      method: 'POST',
      headers,
      body: payload,
    });

    return this.handleResponse<T>(response);
  }

  /**
   * PUT Request
   */
  public static async put<T>(endpoint: string, body: any, config?: RequestConfig): Promise<T> {
    const url = this.buildUrl(endpoint, config?.params);
    const headers = await this.getHeaders(config);

    const isFormData = body instanceof FormData;
    const payload = isFormData ? body : JSON.stringify(body);

    const response = await fetch(url, {
      ...config,
      method: 'PUT',
      headers,
      body: payload,
    });

    return this.handleResponse<T>(response);
  }

  /**
   * PATCH Request
   */
  public static async patch<T>(endpoint: string, body: any, config?: RequestConfig): Promise<T> {
    const url = this.buildUrl(endpoint, config?.params);
    const headers = await this.getHeaders(config);

    const isFormData = body instanceof FormData;
    const payload = isFormData ? body : JSON.stringify(body);

    const response = await fetch(url, {
      ...config,
      method: 'PATCH',
      headers,
      body: payload,
    });

    return this.handleResponse<T>(response);
  }

  /**
   * DELETE Request
   */
  public static async delete<T>(endpoint: string, config?: RequestConfig): Promise<T> {
    const url = this.buildUrl(endpoint, config?.params);
    const headers = await this.getHeaders(config);

    const response = await fetch(url, {
      ...config,
      method: 'DELETE',
      headers,
    });

    return this.handleResponse<T>(response);
  }
}