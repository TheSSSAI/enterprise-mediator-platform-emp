# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-080 |
| Elaboration Date | 2025-01-26 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Reviews Detailed Data Import Validation Repo... |
| As A User Story | As a System Administrator, I want to review a deta... |
| User Persona | System Administrator responsible for system setup ... |
| Business Value | Ensures data integrity during bulk data imports, r... |
| Functional Area | Data Management & Administration |
| Story Theme | System Data Migration and Onboarding |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of Import Report with Mixed Results

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a System Admin has just completed a data import process (either a dry run or final import) for a CSV file containing a mix of valid and invalid records

### 3.1.5 When

the import process concludes

### 3.1.6 Then

the system automatically navigates to the Import Report screen for that specific import job.

### 3.1.7 Validation Notes

Verify the user is redirected to a unique URL for the import job report.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Report Summary Statistics

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the Import Report screen is displayed

### 3.2.5 When

the admin views the top of the screen

### 3.2.6 Then

a summary section is clearly visible containing accurate counts for 'Total Rows Processed', 'Successful Records', 'Records with Warnings', and 'Failed Records'.

### 3.2.7 Validation Notes

Test with a file of 100 rows containing 90 successes, 7 warnings, and 3 failures. The counts must match exactly.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Detailed Row-by-Row Results Table

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the Import Report screen is displayed

### 3.3.5 When

the admin views the detailed results section

### 3.3.6 Then

a paginated table is shown with columns for 'Source Row #', 'Status', and 'Details/Reason'.

### 3.3.7 Validation Notes

Verify that the 'Source Row #' corresponds to the line number in the original uploaded file.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Clear Status and Error Messaging

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

the admin is viewing the detailed results table

### 3.4.5 When

they inspect rows with 'Warning' or 'Failure' statuses

### 3.4.6 Then

the 'Details/Reason' column contains a clear, human-readable message explaining the specific validation issue (e.g., 'Email format is invalid', 'Required field Company Name is missing', 'A client with this name already exists').

### 3.4.7 Validation Notes

Check for specific, actionable error messages, not generic codes.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Filtering Report Results

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the Import Report screen is displayed with a mix of results

### 3.5.5 When

the admin uses the filter control and selects 'Failure'

### 3.5.6 Then

the detailed results table updates to show only the rows with a 'Failure' status.

### 3.5.7 Validation Notes

Test filtering for each status type: Success, Warning, and Failure.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Report Download Functionality

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

the Import Report screen is displayed

### 3.6.5 When

the admin clicks the 'Download Report' button

### 3.6.6 Then

a CSV file is downloaded to the user's machine.

### 3.6.7 And

the downloaded CSV contains all the original data from the uploaded file, plus two additional columns: 'Status' and 'Details/Reason', populated with the results of the import.

### 3.6.8 Validation Notes

Verify the contents of the downloaded file match the report shown on the UI.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Report for a Large File with Pagination

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

an admin has imported a file with over 1000 rows

### 3.7.5 When

the Import Report screen is displayed

### 3.7.6 Then

the detailed results table shows the first page of results (e.g., 100 rows) and provides clear pagination controls to navigate through all pages of the results.

### 3.7.7 Validation Notes

Test that pagination controls (next, previous, page number) work correctly and update the table view.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Report for a 100% Successful Import

### 3.8.3 Scenario Type

Edge_Case

### 3.8.4 Given

an admin has imported a file where all records are valid

### 3.8.5 When

the Import Report screen is displayed

### 3.8.6 Then

the summary shows 'Failed Records: 0' and 'Records with Warnings: 0'.

### 3.8.7 And

all rows in the detailed table have a 'Success' status.

### 3.8.8 Validation Notes

Verify the UI handles the absence of warnings or errors gracefully.

## 3.9.0 Criteria Id

### 3.9.1 Criteria Id

AC-009

### 3.9.2 Scenario

Persisted Report Access

### 3.9.3 Scenario Type

Alternative_Flow

### 3.9.4 Given

an admin has completed an import and is viewing the report

### 3.9.5 When

they navigate away from the report page and later return to the main Data Import section

### 3.9.6 Then

a history of recent import jobs is available, and they can click on a job to view its corresponding report again.

### 3.9.7 Validation Notes

The report data must be persisted on the backend and retrievable by a job ID.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Summary statistics cards/widgets
- Filter dropdown or radio buttons (for Status)
- Sortable, paginated data table
- 'Download Report' button
- Clear visual indicators for status (e.g., colored tags or icons)

## 4.2.0 User Interactions

- User can filter the results table by status (All, Success, Warning, Failure).
- User can sort the table by 'Source Row #' and 'Status'.
- User can navigate between pages of results for large reports.
- Clicking 'Download Report' initiates a file download.

## 4.3.0 Display Requirements

- The report must clearly distinguish between a 'Dry Run' report and a final 'Import' report.
- Status indicators must use color (green/yellow/red) combined with text labels and/or icons for accessibility.
- Error messages must be specific and avoid technical jargon.

## 4.4.0 Accessibility Needs

- Adherence to WCAG 2.1 Level AA.
- The data table must use proper `<table>`, `<thead>`, `<tbody>`, and `<th>` tags with scope attributes.
- All interactive elements (filters, buttons, pagination) must be keyboard-navigable and have accessible names.
- Color alone must not be used to convey status information.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Import reports must be retained for a minimum of 30 days to allow for auditing and review.

### 5.1.3 Enforcement Point

Backend data retention policy/job.

### 5.1.4 Violation Handling

N/A - System policy.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The downloaded report CSV must be named descriptively, including the entity type and timestamp (e.g., 'client-import-report-2025-01-26T10-30-00.csv').

### 5.2.3 Enforcement Point

Backend CSV generation service.

### 5.2.4 Violation Handling

N/A - System logic.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-077

#### 6.1.1.2 Dependency Reason

This story depends on the functionality to upload and process a Client data CSV file, which generates the data for this report.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-078

#### 6.1.2.2 Dependency Reason

This story depends on the functionality to upload and process a Vendor data CSV file, which generates the data for this report.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-079

#### 6.1.3.2 Dependency Reason

The dry run feature is a primary trigger for generating this validation report. The report UI is the main output of a dry run.

## 6.2.0.0 Technical Dependencies

- A backend service/endpoint to process the import and persist the validation results.
- A backend service/endpoint to retrieve the paginated results for a specific import job.
- A backend service/endpoint to generate and serve the downloadable CSV report.
- Frontend data table component capable of sorting, filtering, and pagination.

## 6.3.0.0 Data Dependencies

- Requires the output of the data validation logic from the import process, including row number, status, and error/warning messages.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The report page for an import of up to 10,000 rows must load in under 5 seconds.
- Client-side filtering and sorting on the current page of results must complete in under 500ms.

## 7.2.0.0 Security

- Only users with the 'System Administrator' role can access the data import section and view reports.
- The downloadable CSV report must be sanitized to prevent CSV injection attacks.

## 7.3.0.0 Usability

- The report must be easily scannable, allowing an admin to quickly grasp the overall success rate and locate failures.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The report must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend logic to persist potentially large report result sets efficiently.
- Designing a performant frontend to display and interact with large tables.
- Implementation of a robust and secure CSV download feature.
- Ensuring the reporting logic is generic enough to be reused for different import types (Clients, Vendors, etc.).

## 8.3.0.0 Technical Risks

- Performance degradation when handling very large import files (>50,000 rows). The backend processing must be an asynchronous background job.
- Database bloat if report results are not periodically cleaned up. A TTL or cleanup strategy is required.

## 8.4.0.0 Integration Points

- Integrates with the backend data import/validation service that is built as part of US-077, US-078, and US-079.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify report for a file with a mix of successes, warnings, and failures.
- Verify report for a file with 100% successes.
- Verify report for a file with 100% failures (e.g., missing required header).
- Test pagination, sorting, and filtering functionality.
- Test the CSV download and verify its contents.
- E2E test: Upload a file, navigate to the report, filter for failures, download the report.

## 9.3.0.0 Test Data Needs

- Sample CSV file with valid data.
- Sample CSV file with a mix of common errors (bad email, missing fields, duplicate data).
- A large CSV file (10,000+ rows) for performance testing.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E tests for the core report viewing and interaction workflow are passing
- User interface reviewed and approved for usability and adherence to design specs
- Performance requirements for large reports are verified
- Security requirements (role access) are validated
- Accessibility audit passed (automated and manual checks)
- Documentation for the feature is created/updated in the online help guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a direct dependency for completing the data migration epic. It should be prioritized immediately after the core import/dry-run stories.
- Requires both frontend and backend development effort that can be parallelized once the API contract is defined.

## 11.4.0.0 Release Impact

- Critical for the initial data migration phase (REQ-TRN-002). The system cannot go live without this functionality as there would be no way to verify the success of the initial data load.

