# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-083 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Finance Manager Generates Vendor Performance Repor... |
| As A User Story | As a Finance Manager, I want to generate a filtera... |
| User Persona | Finance Manager. This user has read-only access to... |
| Business Value | Enables data-driven vendor management by providing... |
| Functional Area | Business Intelligence & Reporting |
| Story Theme | Financial Oversight and Analytics |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Generate a report for all active vendors within a specific date range

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in Finance Manager on the 'Reports' page

### 3.1.5 When

I select 'Vendor Performance Report', choose a date range (e.g., last quarter), leave the vendor filter as 'All Active Vendors', and click 'Generate Report'

### 3.1.6 Then

The system displays a table with a row for each active vendor who had activity in that period, showing columns for Vendor Name, Projects Awarded, Proposal Acceptance Rate, On-time Completion Rate, Average Project Value, and Total Value of Awarded Projects. An 'Export to CSV' button becomes active.

### 3.1.7 Validation Notes

Verify that the calculated metrics are correct based on a known test data set for the selected period. The 'Projects Awarded' count should be based on the project award date falling within the filter range.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Filter the report for a single specific vendor

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am a logged-in Finance Manager on the 'Reports' page with the 'Vendor Performance Report' selected

### 3.2.5 When

I select a specific vendor from the vendor filter dropdown, set a date range, and click 'Generate Report'

### 3.2.6 Then

The system displays a table with a single row containing the performance metrics for only the selected vendor.

### 3.2.7 Validation Notes

Confirm that only the data for the chosen vendor is displayed and all other vendors are excluded.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Export the generated report to CSV

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I have successfully generated a Vendor Performance Report on the screen

### 3.3.5 When

I click the 'Export to CSV' button

### 3.3.6 Then

The browser downloads a CSV file named 'vendor_performance_report_YYYY-MM-DD.csv' containing the exact data displayed in the UI table, with appropriate headers.

### 3.3.7 Validation Notes

Open the downloaded file and verify its contents match the on-screen report. Check for correct formatting and headers.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Generate a report for a period with no vendor activity

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am a logged-in Finance Manager on the 'Reports' page

### 3.4.5 When

I select a date range where no projects were awarded or completed and click 'Generate Report'

### 3.4.6 Then

The system displays a clear message such as 'No vendor data found for the selected criteria' instead of an empty table or an error.

### 3.4.7 Validation Notes

Ensure the UI handles the no-data state gracefully without breaking.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Report displays 'N/A' for rates where calculation is not possible

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

A report is generated that includes a vendor who has submitted proposals but none were accepted, and has no completed projects

### 3.5.5 When

I view the report row for that vendor

### 3.5.6 Then

The 'Proposal Acceptance Rate' column shows '0%', and the 'On-time Completion Rate' column shows 'N/A' to avoid division-by-zero errors.

### 3.5.7 Validation Notes

Check the calculation logic to ensure it handles zero denominators correctly and displays a user-friendly value.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Attempt to generate a report with an invalid date range

### 3.6.3 Scenario Type

Error_Condition

### 3.6.4 Given

I am a logged-in Finance Manager on the 'Reports' page

### 3.6.5 When

I select a 'Start Date' that is after the 'End Date'

### 3.6.6 Then

The 'Generate Report' button is disabled, and a validation message 'Start date must be before end date' is displayed next to the date pickers.

### 3.6.7 Validation Notes

Verify client-side validation prevents form submission with invalid dates.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Non-Finance Manager user attempts to access the report page

### 3.7.3 Scenario Type

Error_Condition

### 3.7.4 Given

I am logged in as a user with a role other than 'Finance Manager' or 'System Administrator' (e.g., 'Vendor Contact')

### 3.7.5 When

I attempt to navigate directly to the URL for the Vendor Performance Report

### 3.7.6 Then

The system denies access and redirects me to my default dashboard or an 'Access Denied' page.

### 3.7.7 Validation Notes

Confirm that the route is protected by role-based access control middleware.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Page title: 'Vendor Performance Report'
- Date range picker with 'Start Date' and 'End Date' fields.
- Multi-select dropdown filter for 'Vendors' (with an 'All Active Vendors' default option).
- A 'Generate Report' primary button.
- A loading indicator/spinner to show while data is being fetched.
- A results table with sortable columns.
- An 'Export to CSV' button, disabled until a report is generated.
- A message area for displaying information like 'No data found'.

## 4.2.0 User Interactions

- Selecting dates from a calendar widget.
- Typing to search for vendors in the multi-select dropdown.
- Clicking column headers in the results table to sort the data.
- The page should update asynchronously without a full page reload when the report is generated.

## 4.3.0 Display Requirements

- The report table must display the following columns: Vendor Name, Projects Awarded, Proposal Acceptance Rate (%), On-time Completion Rate (%), Average Project Value (in base currency), Total Value of Awarded Projects (in base currency).
- Currency values should be formatted according to the system's locale settings.
- Percentages should be displayed with one or two decimal places.

## 4.4.0 Accessibility Needs

- All form controls (date pickers, dropdowns, buttons) must have associated labels for screen readers.
- The results table must use proper `<table>`, `<thead>`, `<tbody>`, and `<th>` tags with scope attributes for accessibility.
- The UI must be fully navigable and operable using only a keyboard.
- Color contrast must meet WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-REP-001

### 5.1.2 Rule Description

Proposal Acceptance Rate is calculated as (Number of Accepted Proposals / Total Number of Submitted Proposals) * 100 for the given period. If no proposals were submitted, the rate is N/A.

### 5.1.3 Enforcement Point

Backend report generation service.

### 5.1.4 Violation Handling

N/A - This is a calculation rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-REP-002

### 5.2.2 Rule Description

On-time Completion Rate is calculated as (Number of Projects completed on or before 'planned_end_date' / Total Number of Completed Projects) * 100 for the given period. If no projects were completed, the rate is N/A.

### 5.2.3 Enforcement Point

Backend report generation service.

### 5.2.4 Violation Handling

N/A - This is a calculation rule.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-REP-003

### 5.3.2 Rule Description

Average Project Value is the sum of 'Total Value' for all completed projects divided by the number of completed projects for that vendor in the period.

### 5.3.3 Enforcement Point

Backend report generation service.

### 5.3.4 Violation Handling

N/A - This is a calculation rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

User authentication is required to identify the user's role.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-074

#### 6.1.2.2 Dependency Reason

Role-Based Access Control (RBAC) must be implemented to restrict this feature to authorized roles.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-029

#### 6.1.3.2 Dependency Reason

Project data, including status, value, and associated vendor, must exist to be reported on.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-054

#### 6.1.4.2 Dependency Reason

Proposal data with statuses ('Submitted', 'Accepted') is required to calculate the acceptance rate.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

TBD-ProjectDates

#### 6.1.5.2 Dependency Reason

The Project entity must have 'planned_end_date' and 'actual_completion_date' fields to calculate the on-time completion rate. This may require a new story or modification of an existing one.

## 6.2.0.0 Technical Dependencies

- A reporting UI component library (e.g., for tables and charts).
- A backend service (e.g., Project Service) with an API endpoint for generating report data.
- A server-side library for generating CSV files.

## 6.3.0.0 Data Dependencies

- Access to `Projects`, `Vendors`, and `Proposals` tables in the PostgreSQL database.
- Historical data must be present to generate meaningful reports.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The report generation API call should complete in under 10 seconds for a data set of up to 10,000 projects and 500 vendors.
- UI should remain responsive while the report is being generated in the background.

## 7.2.0.0 Security

- Access to the report generation page and its API endpoint must be strictly limited to users with 'Finance Manager' or 'System Administrator' roles.
- All data must be fetched over HTTPS.
- The CSV export should be sanitized to prevent CSV injection attacks.

## 7.3.0.0 Usability

- The report filters should be intuitive and easy to use.
- The final report should be clearly formatted and easy to read.

## 7.4.0.0 Accessibility

- The feature must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The report page must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend query to aggregate data from multiple tables (Projects, Vendors, Proposals) can be complex.
- Ensuring the performance of the aggregation query on a large dataset requires careful indexing and query optimization.
- The business logic for calculating rates needs to handle edge cases like division by zero.
- A new field, `actual_completion_date`, may need to be added to the Project data model and populated when a project's status moves to 'Completed'.

## 8.3.0.0 Technical Risks

- Poorly optimized database queries could lead to slow report generation times and impact overall system performance.
- Inaccurate data (e.g., missing completion dates) could lead to misleading report metrics.

## 8.4.0.0 Integration Points

- Backend API Gateway to route requests to the appropriate reporting service.
- PostgreSQL database to fetch all required data.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify calculations for each metric with a controlled data set.
- Test report generation with all filter combinations.
- Test the 'no results found' scenario.
- Test the CSV export functionality and file integrity.
- Test role-based access control by attempting to access as an unauthorized user.
- Test UI behavior with a large number of vendors in the filter dropdown.

## 9.3.0.0 Test Data Needs

- A set of vendors with varying levels of activity.
- A vendor with submitted but no accepted proposals.
- A vendor with no completed projects.
- Projects with completion dates both before and after their planned end dates.
- Projects and vendors that fall outside the selected date range to test filtering.

## 9.4.0.0 Testing Tools

- Jest for backend unit/integration tests.
- Playwright for E2E testing.
- A database seeding tool to create the required test data.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage for the new logic and passing
- Integration testing completed successfully
- E2E tests for the user flow are created and passing
- User interface reviewed and approved by the product owner
- Performance requirements verified against a representative dataset
- Security requirements validated (RBAC enforced)
- Accessibility audit passed (WCAG 2.1 AA)
- Documentation for the new report and its metrics is created in the user help guide
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story has a dependency on the Project data model potentially needing a new 'actual_completion_date' field. This should be clarified and addressed before starting development.
- Requires significant backend work for the data aggregation query, which should be tackled first.

## 11.4.0.0 Release Impact

This is a key feature for the Business Intelligence module and a major value-add for financial and operational users. It should be highlighted in release notes.

