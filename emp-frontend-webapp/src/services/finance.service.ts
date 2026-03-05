import { apiClient } from '@/services/api-client';
import type { 
  TransactionDTO, 
  InvoiceDTO, 
  TransactionFilter, 
  PaginatedResponse,
  InitiatePayoutInput,
  GenerateInvoiceInput,
  TaxSettingsDTO,
  ProjectFinancialsDTO
} from '@/lib/types';

/**
 * Service responsible for Financial Management.
 * Handles transactions, invoices, payouts, and financial configuration.
 */
class FinanceService {
  private readonly baseUrl = '/finance';

  /**
   * Retrieves a paginated ledger of financial transactions.
   * @param filters Optional filters for date range, type, status, etc.
   * @returns A paginated list of transactions.
   */
  async getTransactions(filters?: TransactionFilter): Promise<PaginatedResponse<TransactionDTO>> {
    const queryParams = new URLSearchParams();

    if (filters) {
      if (filters.page) queryParams.append('page', filters.page.toString());
      if (filters.limit) queryParams.append('limit', filters.limit.toString());
      if (filters.startDate) queryParams.append('startDate', filters.startDate);
      if (filters.endDate) queryParams.append('endDate', filters.endDate);
      if (filters.type) queryParams.append('type', filters.type);
      if (filters.status) queryParams.append('status', filters.status);
      if (filters.projectId) queryParams.append('projectId', filters.projectId);
    }

    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}/transactions?${queryString}` : `${this.baseUrl}/transactions`;

    return await apiClient.get<PaginatedResponse<TransactionDTO>>(endpoint, ['transactions']);
  }

  /**
   * Generates and sends a client invoice for an awarded project.
   * @param projectId The ID of the project to invoice.
   * @param data Configuration for the invoice generation.
   * @returns The created invoice DTO.
   */
  async generateInvoice(projectId: string, data: GenerateInvoiceInput): Promise<InvoiceDTO> {
    return await apiClient.post<InvoiceDTO>(`/projects/${projectId}/invoice`, data);
  }

  /**
   * Retrieves the financial summary for a specific project (Invoiced, Paid, Pending Payouts).
   * @param projectId The ID of the project.
   * @returns Financial summary DTO.
   */
  async getProjectFinancials(projectId: string): Promise<ProjectFinancialsDTO> {
    return await apiClient.get<ProjectFinancialsDTO>(`/projects/${projectId}/financials`, [`financials-${projectId}`]);
  }

  /**
   * Initiates a payout to a vendor for a completed project or milestone.
   * @param data The payout initiation payload.
   */
  async initiatePayout(data: InitiatePayoutInput): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/payouts/initiate`, data);
  }

  /**
   * Approves a pending payout, triggering the transfer of funds via the gateway.
   * @param payoutId The ID of the payout transaction to approve.
   */
  async approvePayout(payoutId: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/payouts/${payoutId}/approve`, {});
  }

  /**
   * Rejects a pending payout.
   * @param payoutId The ID of the payout transaction.
   * @param reason The reason for rejection.
   */
  async rejectPayout(payoutId: string, reason: string): Promise<void> {
    return await apiClient.post<void>(`${this.baseUrl}/payouts/${payoutId}/reject`, { reason });
  }

  /**
   * Retrieves the system-wide tax configuration settings.
   * @returns The current tax settings.
   */
  async getTaxSettings(): Promise<TaxSettingsDTO> {
    return await apiClient.get<TaxSettingsDTO>(`${this.baseUrl}/config/tax`, ['tax-settings']);
  }

  /**
   * Updates the system-wide tax configuration settings.
   * @param data The new tax settings.
   */
  async updateTaxSettings(data: TaxSettingsDTO): Promise<TaxSettingsDTO> {
    return await apiClient.put<TaxSettingsDTO>(`${this.baseUrl}/config/tax`, data);
  }

  /**
   * Exports the transaction report as a CSV file.
   * @param filters Filters to apply to the export.
   * @returns A blob containing the CSV data.
   */
  async exportTransactionsCsv(filters?: TransactionFilter): Promise<Blob> {
    const queryParams = new URLSearchParams();
    if (filters) {
        if (filters.startDate) queryParams.append('startDate', filters.startDate);
        if (filters.endDate) queryParams.append('endDate', filters.endDate);
        if (filters.type) queryParams.append('type', filters.type);
    }
    const queryString = queryParams.toString();
    const endpoint = queryString ? `${this.baseUrl}/reports/transactions?${queryString}` : `${this.baseUrl}/reports/transactions`;
    
    // Note: This uses a raw fetch or specialized method in apiClient if available for Blob
    // Assuming apiClient has a method for blob or we handle it via generic get
    return await apiClient.get<Blob>(endpoint); 
  }
}

export const financeService = new FinanceService();