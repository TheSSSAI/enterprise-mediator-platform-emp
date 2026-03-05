# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-082 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Finance Manager Generates Project Profitability Re... |
| As A User Story | As a Finance Manager, I want to generate a filtera... |
| User Persona | Finance Manager: A user responsible for financial ... |
| Business Value | Provides critical business intelligence on project... |
| Functional Area | Business Intelligence & Reporting |
| Story Theme | Financial Management and Oversight |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Generate report with default filters

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a Finance Manager and have navigated to the 'Reports' section

### 3.1.5 When

I select the 'Project Profitability Report'

### 3.1.6 Then

The system displays a report table for all projects with a 'Completed' status within the default date range (e.g., past 90 days).

### 3.1.7 Validation Notes

Verify the report page loads and displays data. The default filters should be clearly indicated.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Report data columns and calculations are correct

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The Project Profitability Report is displayed

### 3.2.5 When

I view the report table

### 3.2.6 Then

The table must contain the following columns: 'Project Name', 'Client Name', 'Completion Date', 'Total Invoice Amount', 'Total Vendor Payout', 'Net Profit', and 'Profit Margin (%)'.

### 3.2.7 Validation Notes

For a test project with a $10,000 invoice and $8,000 payout, Net Profit must be $2,000 and Profit Margin must be 20.00%. All monetary values should be formatted correctly (e.g., two decimal places, currency symbol/code).

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Apply filters and view updated report

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am viewing the Project Profitability Report

### 3.3.5 When

I apply a custom date range filter, select a specific client from a dropdown, and click 'Apply Filters'

### 3.3.6 Then

The report table and summary totals update to show only the data for projects that match all the selected criteria.

### 3.3.7 Validation Notes

Test with various filter combinations to ensure the logic is correct and the UI updates instantly.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Export filtered report to CSV

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I have generated a Project Profitability Report with specific filters applied

### 3.4.5 When

I click the 'Export to CSV' button

### 3.4.6 Then

A CSV file is downloaded to my computer with a name like 'Project_Profitability_Report_YYYY-MM-DD.csv'.

### 3.4.7 Validation Notes

Open the downloaded CSV and verify its contents match the filtered data displayed on the screen, including column headers.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Report handles no matching data

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am viewing the Project Profitability Report

### 3.5.5 When

I apply filters that result in no matching projects

### 3.5.6 Then

The report table area displays a user-friendly message, such as 'No projects match the selected criteria.'

### 3.5.7 Validation Notes

Verify the summary totals show '0' or 'N/A' and that the 'Export to CSV' button is disabled.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Report displays summary totals

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

The Project Profitability Report is displayed with data

### 3.6.5 When

I view the area below the report table

### 3.6.6 Then

The system displays summary totals for 'Total Invoice Amount', 'Total Vendor Payout', 'Total Net Profit', and calculates the 'Average Profit Margin' for the displayed dataset.

### 3.6.7 Validation Notes

Manually calculate the totals for a small dataset and verify they match the system's calculations.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Access to report is restricted

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am logged in as a user without the 'Finance Manager' or 'System Administrator' role (e.g., 'Client Contact')

### 3.7.5 When

I attempt to access the URL for the Project Profitability Report directly

### 3.7.6 Then

The system prevents access and displays a '403 Forbidden' or 'Access Denied' error page.

### 3.7.7 Validation Notes

Confirm that only authorized roles can view the report navigation link and access the page.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Report handles multi-currency transactions

### 3.8.3 Scenario Type

Alternative_Flow

### 3.8.4 Given

The system's base currency is set to USD

### 3.8.5 And

A project was invoiced in EUR and paid out in GBP

### 3.8.6 When

I generate the Project Profitability Report including this project

### 3.8.7 Then

All monetary values for that project are displayed in the base currency (USD), converted using the exchange rates stored at the time of each transaction.

### 3.8.8 Validation Notes

Verify the report clearly states 'All values in USD' and that the converted values are correct based on stored exchange rates.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Date range picker with presets (e.g., Last 30 Days, Last Quarter) and custom range selection.
- Multi-select or searchable dropdown for 'Client'.
- Multi-select dropdown for 'Project Status' (defaulting to 'Completed').
- An 'Apply Filters' button.
- A 'Reset Filters' button.
- A data table with sortable columns.
- Pagination controls for the data table (e.g., 25 results per page).
- An 'Export to CSV' button.
- A summary section for totals below the table.

## 4.2.0 User Interactions

- Clicking a column header sorts the table by that column.
- Applying filters updates the table and summary totals without a full page reload.
- The UI should provide loading indicators while data is being fetched or recalculated.

## 4.3.0 Display Requirements

- The report must have a clear title: 'Project Profitability Report'.
- All monetary values must be formatted to two decimal places and clearly indicate the base currency (e.g., 'All values in USD').
- Dates should be displayed in a consistent, user-friendly format (e.g., YYYY-MM-DD).

## 4.4.0 Accessibility Needs

- The report must adhere to WCAG 2.1 Level AA standards.
- All filter controls and table elements must be keyboard navigable and compatible with screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Profitability is calculated as (Total Invoice Amount - Total Vendor Payout).

### 5.1.3 Enforcement Point

Backend report generation service.

### 5.1.4 Violation Handling

N/A - Calculation rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Profit Margin is calculated as ((Net Profit / Total Invoice Amount) * 100). If Invoice Amount is zero, margin is zero.

### 5.2.3 Enforcement Point

Backend report generation service.

### 5.2.4 Violation Handling

N/A - Calculation rule. Must handle division by zero.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

Only users with 'Finance Manager' or 'System Administrator' roles can access this report.

### 5.3.3 Enforcement Point

API Gateway and backend service middleware.

### 5.3.4 Violation Handling

Return a 403 Forbidden HTTP status code.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-058

#### 6.1.1.2 Dependency Reason

Requires completed client payment transactions to calculate 'Total Invoice Amount'.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-061

#### 6.1.2.2 Dependency Reason

Requires completed vendor payout transactions to calculate 'Total Vendor Payout'.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-041

#### 6.1.3.2 Dependency Reason

Requires the ability to set a project's status to 'Completed' to filter the report data correctly.

## 6.2.0.0 Technical Dependencies

- A shared UI component for a filterable, sortable, and paginated data table.
- A backend service/module for generating CSV files.
- The database schema must be optimized for joining and aggregating data from Projects, Clients, and Transactions tables.

## 6.3.0.0 Data Dependencies

- Accurate and complete transaction records (payments and payouts) with associated project IDs.
- Accurate project status and completion dates.
- Stored exchange rates for any transactions not in the system's base currency.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 95th percentile for report generation API calls should be under 3 seconds, even with a year's worth of data.
- The UI must remain responsive while filters are being applied.

## 7.2.0.0 Security

- Access to the report's API endpoint must be strictly enforced by role-based access control (RBAC) at the API gateway level.
- Data should be transmitted over HTTPS.

## 7.3.0.0 Usability

- The report interface should be clean and intuitive, allowing a non-technical finance user to easily generate and understand the data.
- Filter controls should be easy to use and understand.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards as per REQ-INT-001.

## 7.5.0.0 Compatibility

- The report must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The database query to aggregate financial data across multiple tables (Projects, Clients, Transactions) can be complex and needs to be highly performant.
- Correctly handling multi-currency conversions based on historical exchange rates adds significant complexity.
- Building a reusable, performant, and accessible data table component with filtering, sorting, and pagination on the frontend.

## 8.3.0.0 Technical Risks

- Poorly optimized database queries could lead to slow report generation times as data volume grows.
- Inaccurate or missing transaction data could lead to incorrect report figures, undermining user trust.

## 8.4.0.0 Integration Points

- Backend Project Service to get project and client details.
- Backend Payment Service to get transaction data.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security

## 9.2.0.0 Test Scenarios

- Verify report generation with various filter combinations.
- Validate the accuracy of all calculations (Net Profit, Margin) with a known dataset.
- Test the CSV export functionality and validate the file content.
- Test the edge case of no data matching the filters.
- Test role-based access control by attempting to access as an unauthorized user.
- Test with projects involving multiple currencies to validate conversion logic.

## 9.3.0.0 Test Data Needs

- A set of projects with 'Completed' and other statuses.
- Multiple client and vendor records.
- A history of transaction records (payments and payouts) linked to projects, including some in different currencies.
- Projects with zero invoice amount or zero payout to test calculation edge cases.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A database seeding tool to create consistent test data.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other engineer
- Unit and integration tests implemented with at least 80% code coverage and all passing
- E2E test script covering the primary happy path (filter, view, export) is created and passing
- User interface is responsive and has been reviewed for usability and accessibility compliance
- Performance testing confirms report generation meets latency requirements
- Security testing confirms endpoint is properly secured via RBAC
- API documentation (OpenAPI) is updated for the new report endpoint
- Story has been deployed and verified in the staging environment by QA and the Product Owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a key feature for the Finance Manager persona and provides significant business value.
- Dependent on the completion of core transaction and project lifecycle user stories.
- Requires both backend (API, query logic) and frontend (UI components) development effort.

## 11.4.0.0 Release Impact

This is a major feature for the financial management and business intelligence capabilities of the platform. Its release will be highlighted to stakeholders.

