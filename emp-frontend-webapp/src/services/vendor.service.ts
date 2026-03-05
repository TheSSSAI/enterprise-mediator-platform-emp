import { apiClient } from '@/services/api-client';
import type { 
  VendorDTO, 
  VendorDetailDTO, 
  CreateVendorInput, 
  UpdateVendorInput, 
  VendorFilter, 
  PaginatedResponse,
  VendorRecommendationDTO
} from '@/lib/types';

/**
 * Service responsible for Vendor Entity Management.
 * Handles vendor onboarding, profile management, and lifecycle status.
 */
class VendorService {
  private readonly baseUrl = '/vendors';

  /**
   * Retrieves a paginated list of vendors based on search and filter criteria.
   * @param filters Optional filtering and pagination parameters.
   * @returns A paginated list of vendors.
   */
  async getVendors(filters?: VendorFilter): Promise<PaginatedResponse<VendorDTO>> {
    const queryParams = new URLSearchParams();

    if (filters) {
      if (filters.page) queryParams.append('page', filters.page.toString());
      if (filters.limit) queryParams.append('limit', filters.limit.toString());
      if (filters.search) queryParams.append('search', filters.search);
      if (filters.status) queryParams.append('status', filters.status);
      if (filters.skills && filters.skills.length > 0) {
        queryParams.append('skills', filters.skills.join(','));
      }
    }

    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}?${queryString}` : this.baseUrl;

    return await apiClient.get<PaginatedResponse<VendorDTO>>(endpoint, ['vendors']);
  }

  /**
   * Retrieves detailed profile information for a specific vendor.
   * @param id The unique identifier of the vendor.
   * @returns The detailed vendor profile DTO.
   */
  async getVendorById(id: string): Promise<VendorDetailDTO> {
    return await apiClient.get<VendorDetailDTO>(`${this.baseUrl}/${id}`, [`vendor-${id}`]);
  }

  /**
   * Creates a new vendor profile in the system.
   * @param data The vendor creation payload.
   * @returns The created vendor DTO.
   */
  async createVendor(data: CreateVendorInput): Promise<VendorDTO> {
    return await apiClient.post<VendorDTO>(this.baseUrl, data);
  }

  /**
   * Updates an existing vendor's profile information.
   * @param id The unique identifier of the vendor.
   * @param data The partial update payload.
   * @returns The updated vendor DTO.
   */
  async updateVendor(id: string, data: UpdateVendorInput): Promise<VendorDTO> {
    return await apiClient.patch<VendorDTO>(`${this.baseUrl}/${id}`, data);
  }

  /**
   * Activates a vendor, making them eligible for project matching.
   * Only applicable for vendors in 'Pending Vetting' status.
   * @param id The unique identifier of the vendor.
   */
  async activateVendor(id: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/${id}/activate`, {});
  }

  /**
   * Deactivates a vendor, preventing them from receiving new project opportunities.
   * @param id The unique identifier of the vendor.
   */
  async deactivateVendor(id: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/${id}/deactivate`, {});
  }

  /**
   * Invites a new contact user to an existing vendor organization.
   * @param vendorId The ID of the vendor organization.
   * @param email The email address of the contact to invite.
   */
  async inviteContact(vendorId: string, email: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/${vendorId}/contacts/invite`, { email });
  }

  /**
   * Retrieves AI-generated vendor recommendations for a specific project.
   * @param projectId The project ID to match against.
   * @returns A list of recommended vendors with similarity scores.
   */
  async getRecommendations(projectId: string): Promise<VendorRecommendationDTO[]> {
    return await apiClient.get<VendorRecommendationDTO[]>(
      `/projects/${projectId}/recommendations`, 
      [`recommendations-${projectId}`]
    );
  }
}

export const vendorService = new VendorService();