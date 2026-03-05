import { apiClient } from '@/services/api-client';
import type { 
  ProjectDTO, 
  ProjectDetailDTO, 
  CreateProjectInput, 
  ProjectFilter, 
  PaginatedResponse,
  SowStatusDTO,
  UpdateProjectStatusInput
} from '@/lib/types';

/**
 * Service responsible for Project Lifecycle Management.
 * Handles project creation, retrieval, status updates, and SOW processing.
 */
class ProjectService {
  private readonly baseUrl = '/projects';

  /**
   * Retrieves a paginated list of projects based on filter criteria.
   * @param filters Optional filtering and pagination parameters.
   * @returns A paginated list of projects.
   */
  async getProjects(filters?: ProjectFilter): Promise<PaginatedResponse<ProjectDTO>> {
    const queryParams = new URLSearchParams();
    
    if (filters) {
      if (filters.page) queryParams.append('page', filters.page.toString());
      if (filters.limit) queryParams.append('limit', filters.limit.toString());
      if (filters.status) queryParams.append('status', filters.status);
      if (filters.clientId) queryParams.append('clientId', filters.clientId);
      if (filters.search) queryParams.append('search', filters.search);
    }

    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}?${queryString}` : this.baseUrl;

    // Utilize Next.js caching with specific tags for revalidation
    return await apiClient.get<PaginatedResponse<ProjectDTO>>(endpoint, ['projects']);
  }

  /**
   * Retrieves detailed information for a specific project.
   * @param id The unique identifier of the project.
   * @returns The detailed project DTO.
   */
  async getProjectById(id: string): Promise<ProjectDetailDTO> {
    return await apiClient.get<ProjectDetailDTO>(`${this.baseUrl}/${id}`, [`project-${id}`]);
  }

  /**
   * Creates a new project in the system.
   * @param data The project creation payload.
   * @returns The created project DTO.
   */
  async createProject(data: CreateProjectInput): Promise<ProjectDTO> {
    return await apiClient.post<ProjectDTO>(this.baseUrl, data);
  }

  /**
   * Updates the status of an existing project (e.g., Active, On Hold, Cancelled).
   * @param id The unique identifier of the project.
   * @param status The new status to apply.
   */
  async updateProjectStatus(id: string, status: UpdateProjectStatusInput): Promise<void> {
    return await apiClient.patch<void>(`${this.baseUrl}/${id}/status`, status);
  }

  /**
   * Uploads a Statement of Work (SOW) document for AI processing.
   * @param projectId The ID of the project the SOW belongs to.
   * @param formData The FormData object containing the file.
   */
  async uploadSow(projectId: string, formData: FormData): Promise<void> {
    // Note: The ApiClient handles the content-type header for FormData automatically
    // usually by letting the browser/runtime set the boundary.
    return await apiClient.postForm<void>(`${this.baseUrl}/${projectId}/sow`, formData);
  }

  /**
   * Retrieves the current processing status of an uploaded SOW.
   * @param projectId The ID of the project.
   * @returns The SOW processing status (e.g., PROCESSING, PROCESSED, FAILED).
   */
  async getSowStatus(projectId: string): Promise<SowStatusDTO> {
    return await apiClient.get<SowStatusDTO>(`${this.baseUrl}/${projectId}/sow/status`, [`sow-status-${projectId}`]);
  }

  /**
   * Retrieves the sanitized and extracted data from a processed SOW.
   * @param projectId The ID of the project.
   * @returns The extracted SOW data for review.
   */
  async getExtractedSowData(projectId: string): Promise<any> {
    return await apiClient.get<any>(`${this.baseUrl}/${projectId}/sow/data`, [`sow-data-${projectId}`]);
  }

  /**
   * Updates the extracted SOW data after manual review (Human-in-the-Loop).
   * @param projectId The ID of the project.
   * @param data The corrected SOW data.
   */
  async updateBrief(projectId: string, data: any): Promise<void> {
    return await apiClient.put<void>(`${this.baseUrl}/${projectId}/brief`, data);
  }

  /**
   * Finalizes the Project Brief and triggers vendor matching.
   * @param projectId The ID of the project.
   */
  async approveBrief(projectId: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/${projectId}/brief/approve`, {});
  }

  /**
   * Distributes the approved Project Brief to selected vendors.
   * @param projectId The ID of the project.
   * @param vendorIds Array of vendor IDs to invite.
   */
  async distributeBrief(projectId: string, vendorIds: string[]): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/${projectId}/distribute`, { vendorIds });
  }
}

export const projectService = new ProjectService();