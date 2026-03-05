# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-084 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Finance Manager Generates Client Activity Report |
| As A User Story | As a Finance Manager, I want to generate a detaile... |
| User Persona | Finance Manager. This user has read-only access to... |
| Business Value | Provides a clear, on-demand financial overview of ... |
| Functional Area | Reporting and Business Intelligence |
| Story Theme | Financial Oversight and Reporting |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Generate report for a client with activity in the specified date range

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a Finance Manager and am on the 'Reports' page

### 3.1.5 When

I select the 'Client Activity Report', choose a client with multiple completed projects from the client selector, define a date range that includes those projects, and click 'Generate Report'

### 3.1.6 Then

The system displays an on-screen report containing a summary section with the Client's Name, Date Range, Total Spend (sum of all paid invoices in the period), and Total Number of Projects. A detailed project list table is also displayed with columns for 'Project Name', 'Project Status', 'Start Date', 'End Date', and 'Total Invoiced Amount'.

### 3.1.7 Validation Notes

Verify that the 'Total Spend' correctly sums all paid invoices within the date range. Confirm the project list only contains projects whose start or end dates fall within the selected range.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Export the generated report to CSV

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I have successfully generated a Client Activity Report

### 3.2.5 When

I click the 'Export to CSV' button

### 3.2.6 Then

The browser initiates a download of a CSV file named 'Client_Activity_[ClientName]_[StartDate]_[EndDate].csv'. The contents of the CSV must match the data and headers of the on-screen project list table.

### 3.2.7 Validation Notes

Open the downloaded CSV and verify its structure and data integrity against the on-screen report. Check for correct formatting of dates and currency values.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Generate report for a client with no activity in the date range

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am logged in as a Finance Manager on the 'Reports' page

### 3.3.5 When

I select a client and a date range in which they have no project or financial activity

### 3.3.6 Then

The system displays the summary section with 'Total Spend: $0.00' and 'Total Projects: 0', and a message 'No project activity found for this client in the selected date range' is shown in the project list area. The 'Export to CSV' button is disabled.

### 3.3.7 Validation Notes

Test with a valid client but a date range far in the future or past where no activity exists.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Attempt to generate a report with an invalid date range

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

I am on the Client Activity Report generation page

### 3.4.5 When

I select a 'Start Date' that is after the 'End Date' and click 'Generate Report'

### 3.4.6 Then

The system displays a validation error message, such as 'The Start Date cannot be after the End Date', and does not attempt to generate the report.

### 3.4.7 Validation Notes

Verify that no API call is made to the backend when the date range is invalid.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Report generation performance

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am generating a report for a client with a large number of projects (e.g., 500) over a multi-year period

### 3.5.5 When

I click 'Generate Report'

### 3.5.6 Then

The system displays a loading indicator while processing and the final report is rendered within 5 seconds.

### 3.5.7 Validation Notes

This must be tested in a staging environment with a representative dataset. Monitor API response time.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Reports' section in the main navigation.
- A form with a searchable dropdown/autocomplete for client selection.
- Date range selector with presets ('Last 30 Days', 'This Quarter', 'YTD') and a custom range picker.
- A 'Generate Report' button.
- A loading state indicator (e.g., spinner).
- A summary data display area.
- A paginated data table for the project list.
- An 'Export to CSV' button.

## 4.2.0 User Interactions

- Typing in the client selector should filter the list of clients.
- Selecting a date preset should automatically populate the custom date fields.
- Clicking 'Generate Report' should trigger data fetching and display a loading state.
- The 'Export to CSV' button is disabled until a report with data is successfully generated.

## 4.3.0 Display Requirements

- All financial figures must be formatted according to the user's locale and currency settings.
- Dates must be displayed in a consistent, user-friendly format (e.g., YYYY-MM-DD).
- The report must clearly state the client and date range for which it was generated.

## 4.4.0 Accessibility Needs

- All form controls must have associated labels and be fully keyboard navigable.
- The report data table must use proper semantic HTML (<thead>, <tbody>, <th> with scope attributes) for screen reader compatibility.
- Meets WCAG 2.1 Level AA standards as per REQ-INT-001.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

The 'Total Spend' calculation must only include invoices that have a 'Paid' status.

### 5.1.3 Enforcement Point

Backend data aggregation service.

### 5.1.4 Violation Handling

Invoices with other statuses (e.g., 'Pending', 'Overdue') are excluded from the sum. No error is thrown.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Access to this report is restricted to users with the 'Finance Manager' or 'System Administrator' role.

### 5.2.3 Enforcement Point

API Gateway and backend service authorization middleware.

### 5.2.4 Violation Handling

An unauthorized user attempting to access the report UI or API endpoint will receive a 403 Forbidden error.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-012

#### 6.1.1.2 Dependency Reason

Client data must exist in the system to be selected for a report.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-029

#### 6.1.2.2 Dependency Reason

Project data must exist and be associated with clients to appear in the report.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-058

#### 6.1.3.2 Dependency Reason

Paid invoice and transaction data are required to accurately calculate 'Total Spend'.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint for fetching and aggregating report data.
- A shared frontend component for date range selection.
- A backend library for CSV file generation.

## 6.3.0.0 Data Dependencies

- Read access to the Client, Project, and Transaction data models/services.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- API response time for report generation should be under 5 seconds for 95% of requests for clients with up to 500 projects in the specified range.

## 7.2.0.0 Security

- The API endpoint must enforce role-based access control (RBAC) to ensure only authorized users can generate reports.
- Data must be fetched securely, preventing any possibility of accessing data for an unauthorized client (e.g., via parameter tampering).

## 7.3.0.0 Usability

- The report generation interface must be intuitive, requiring minimal training for a Finance Manager.
- Error messages must be clear and actionable.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The report generation and display must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The database query for data aggregation can be complex, requiring joins across multiple tables and careful filtering.
- Ensuring the query is performant at scale requires proper database indexing on foreign keys and date fields.
- Frontend state management for handling user inputs, loading states, and report data.
- Backend implementation of CSV export functionality.

## 8.3.0.0 Technical Risks

- A poorly optimized database query could lead to slow performance and API timeouts.
- Potential for inaccurate financial calculations if the aggregation logic does not correctly handle all edge cases (e.g., refunds, multi-currency transactions).

## 8.4.0.0 Integration Points

- Backend: Integrates with the PostgreSQL database, specifically the tables/services for Clients, Projects, and Transactions.
- Frontend: Integrates with the application's authentication service to verify user role.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance

## 9.2.0.0 Test Scenarios

- Verify report generation for a client with projects inside, outside, and spanning the selected date range.
- Verify the accuracy of the 'Total Spend' calculation with various transaction types.
- Test the CSV export for content, naming convention, and format.
- Test the UI response and error handling for invalid inputs.
- Perform an E2E test using Playwright that covers the entire user flow from login to downloading the CSV.

## 9.3.0.0 Test Data Needs

- A set of test clients with varying amounts of project and transaction history.
- A client with no activity.
- Projects with different statuses ('Active', 'Completed', 'Cancelled').
- Transactions in multiple currencies to test aggregation logic (if applicable).

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage
- E2E tests for critical paths are created and passing
- User interface reviewed for usability and adherence to design specifications
- Performance requirements verified in a staging environment
- Security requirements (RBAC) validated
- Online help documentation for generating reports is created or updated
- Story deployed and verified in the staging environment by QA or the Product Owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story provides significant value to the Finance persona and is a core feature of the reporting module.
- Requires both backend (API, query) and frontend (UI) development, which can be done in parallel.
- Availability of realistic test data in the development environment is crucial for accurate implementation and testing.

## 11.4.0.0 Release Impact

This is a key feature for the initial release to financial stakeholders. It demonstrates the platform's ability to provide business intelligence beyond simple transaction management.

