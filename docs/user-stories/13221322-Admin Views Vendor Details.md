# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-024 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Vendor Details |
| As A User Story | As a System Administrator, I want to view a compre... |
| User Persona | System Administrator. This role requires a complet... |
| Business Value | Provides a single source of truth for vendor infor... |
| Functional Area | Entity Management |
| Story Theme | Vendor Lifecycle Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

View details of an active vendor with project history

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a System Administrator and am on the Vendor List page

### 3.1.5 When

I click on the name of an active vendor with a history of projects and proposals

### 3.1.6 Then

I am navigated to that vendor's detail page, and the page displays the vendor's core information: Company Name, Address, and Vetting Status ('Active').

### 3.1.7 Validation Notes

Verify all static fields are present and match the database record for the selected vendor.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Vendor details page displays expertise and contacts

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing a vendor's detail page

### 3.2.5 When

The page loads

### 3.2.6 Then

The vendor's 'Areas of Expertise' are displayed as a list of tags, and a list of 'Associated Contacts' with their Name and Email is shown.

### 3.2.7 Validation Notes

Check that expertise tags and contact details are correctly fetched from the related database tables.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Vendor details page displays calculated performance metrics

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am viewing a vendor's detail page

### 3.3.5 When

The page loads

### 3.3.6 Then

A 'Performance Metrics' section displays calculated values for 'Total Projects Awarded', 'Proposal Acceptance Rate', 'On-Time Completion Rate', and 'Average Project Value'.

### 3.3.7 Validation Notes

Backend logic for calculating these metrics must be validated against a known data set. For example, if a vendor submitted 4 proposals and 2 were accepted, the rate should be 50%.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Vendor details page displays project history

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am viewing a vendor's detail page

### 3.4.5 When

The page loads

### 3.4.6 Then

A 'Project History' section displays a table of projects awarded to the vendor, including columns for Project Name, Client Name, Status, and End Date.

### 3.4.7 Validation Notes

The table should be sortable by each column. Verify the list matches the projects associated with the vendor in the database.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

View details of a new vendor with no history

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

I am logged in as a System Administrator

### 3.5.5 When

I view the detail page for a newly created vendor with a 'Pending Vetting' status and no project history

### 3.5.6 Then

The 'Performance Metrics' and 'Project History' sections display a user-friendly message, such as 'No performance data available yet'.

### 3.5.7 Validation Notes

Ensure the UI handles the absence of data gracefully without errors or empty sections.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

View details of a deactivated vendor

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am logged in as a System Administrator

### 3.6.5 When

I view the detail page for a vendor with a 'Deactivated' status

### 3.6.6 Then

The page clearly displays the 'Deactivated' status, and all historical data (metrics, projects) remains visible for archival purposes.

### 3.6.7 Validation Notes

A prominent banner or status indicator should be used to highlight the deactivated state.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Sensitive payment information is masked

### 3.7.3 Scenario Type

Happy_Path

### 3.7.4 Given

I am viewing a vendor's detail page

### 3.7.5 When

I look at the 'Payment Details' section

### 3.7.6 Then

Sensitive information like bank account numbers are partially masked (e.g., '********1234').

### 3.7.7 Validation Notes

Verify that the full, unmasked data is never sent to the client-side application.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Attempt to view a non-existent vendor

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

I am logged in as a System Administrator

### 3.8.5 When

I navigate to a vendor detail URL with an ID that does not exist

### 3.8.6 Then

The system displays a '404 - Vendor Not Found' error page with a link to return to the Vendor List.

### 3.8.7 Validation Notes

The API should return a 404 status code, which the frontend handles by showing the error page.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Page title with Vendor Company Name
- Status badge (e.g., Active, Pending, Deactivated)
- Sections/cards for 'Company Information', 'Areas of Expertise', 'Associated Contacts', 'Payment Details', 'Performance Metrics', and 'Project History'
- 'Back to Vendor List' link/button
- 'Edit Vendor' button (navigates to functionality from US-025)
- Sortable table for Project History

## 4.2.0 User Interactions

- Clicking on a vendor in the list view navigates to this detail page.
- Email addresses for contacts are clickable `mailto:` links.
- The project history table can be sorted by clicking on column headers.

## 4.3.0 Display Requirements

- The layout must be clean, organized, and easy to scan.
- Performance metrics should be displayed clearly, perhaps as stat cards.
- Sensitive data (e.g., bank account numbers) must always be displayed in a masked format.

## 4.4.0 Accessibility Needs

- The page must be navigable using a keyboard.
- All sections must have proper headings (<h1>, <h2>, etc.) for screen reader navigation.
- All interactive elements must have clear focus states.
- Adheres to WCAG 2.1 Level AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only System Administrators and Finance Managers can view the detailed profile of any vendor.

### 5.1.3 Enforcement Point

API Gateway and Backend Service (API endpoint for GET /vendors/{id})

### 5.1.4 Violation Handling

The API returns a 403 Forbidden status code. The UI redirects the user to an 'Access Denied' page.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Deactivating a vendor does not delete their historical data; it remains accessible for reporting and auditing.

### 5.2.3 Enforcement Point

Business Logic Layer

### 5.2.4 Violation Handling

N/A - This is a design principle. The deactivation process should only change a status flag, not delete records.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-020

#### 6.1.1.2 Dependency Reason

A vendor profile must be creatable before its details can be viewed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-021

#### 6.1.2.2 Dependency Reason

The vendor list page is the primary entry point for navigating to the detail view.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-006

#### 6.1.3.2 Dependency Reason

User must be authenticated to access any system data.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-074

#### 6.1.4.2 Dependency Reason

Role-Based Access Control (RBAC) must be in place to enforce viewing permissions.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/vendors/{id}) that aggregates all necessary vendor data.
- Database schema for Vendors, Users, Projects, and Proposals must be defined.
- Authentication service (AWS Cognito) to verify user roles.

## 6.3.0.0 Data Dependencies

- Requires access to vendor, user, project, and proposal data to populate the view and calculate metrics.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for the vendor detail endpoint must be under 250ms (p95).
- The page's Largest Contentful Paint (LCP) should be under 2.5 seconds.

## 7.2.0.0 Security

- Access to this page and its underlying API must be strictly controlled by the RBAC model.
- Sensitive payment information must be masked on the backend before being sent to the client.
- The endpoint should be protected against unauthorized access attempts.

## 7.3.0.0 Usability

- The information architecture must be logical and intuitive, allowing admins to find information quickly.
- The page must be responsive and usable on desktop and tablet screen sizes.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- The backend query to aggregate data from multiple tables (vendors, users, projects, proposals) can be complex and must be optimized for performance.
- The logic for calculating performance metrics needs to be robust and handle edge cases (e.g., division by zero if a vendor has no proposals).
- Ensuring proper data masking for sensitive information requires careful implementation on the backend.

## 8.3.0.0 Technical Risks

- Potential for slow database queries (N+1 problem) if data aggregation is not handled efficiently.
- The logic for performance metrics might become complex, requiring thorough testing.

## 8.4.0.0 Integration Points

- User Service (to fetch contact details)
- Project Service (to fetch project history and proposal data for metrics)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify all data fields for an active vendor.
- Verify empty states for a new vendor.
- Verify the display for a deactivated vendor.
- Verify data masking of payment details.
- Test role-based access control by attempting to access as an unauthorized user.
- Test the 404 error page for a non-existent vendor ID.
- Verify sorting functionality on the project history table.

## 9.3.0.0 Test Data Needs

- A test vendor with no history.
- A test vendor with a full history of multiple projects (some on-time, some late) and proposals (some accepted, some rejected).
- A test vendor with a 'Deactivated' status.
- User accounts with 'System Administrator' and a non-privileged role.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% coverage for new logic
- E2E tests for critical paths are implemented and passing
- User interface reviewed and approved by the product owner/designer
- Performance requirements (API latency, LCP) are verified
- Security requirements (RBAC, data masking) are validated
- Accessibility scan passed with no critical issues
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- The backend API contract should be defined early to allow for parallel frontend and backend development.
- Requires test data that accurately reflects different vendor states.

## 11.4.0.0 Release Impact

This is a core feature for vendor management and is essential for the MVP.

