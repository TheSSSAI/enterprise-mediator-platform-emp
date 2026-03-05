# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-051 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Proposal Dashboard for a Specific Proj... |
| As A User Story | As a System Administrator, I want to view a dedica... |
| User Persona | System Administrator. This also applies to the Fin... |
| Business Value | Provides a centralized command center for the vend... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Displaying proposals for a project that has received submissions

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator and I am viewing a project that has received one or more proposals

### 3.1.5 When

I navigate to the 'Proposals' tab or section for that project

### 3.1.6 Then

I see a dashboard view with a clear title indicating the project name, such as 'Proposals for [Project Name]'.

### 3.1.7 And

Each entry must have an actionable control, such as a 'View Details' button, to navigate to the full proposal view (US-052).

### 3.1.8 Validation Notes

Verify that the data displayed for each proposal matches the data submitted by the vendor. Test with a project having a single proposal and another with multiple proposals.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dashboard view for a project with no submitted proposals

### 3.2.3 Scenario Type

Edge_Case

### 3.2.4 Given

I am a logged-in System Administrator and I am viewing a project for which no proposals have been submitted yet

### 3.2.5 When

I navigate to the 'Proposals' tab or section for that project

### 3.2.6 Then

The dashboard displays a clear, user-friendly message indicating that no proposals have been received, such as 'No proposals have been submitted for this project yet.'

### 3.2.7 And

The summary count should show '0 Proposals Received'.

### 3.2.8 Validation Notes

Create a new project, distribute the brief, but do not submit any proposals. Navigate to the dashboard and verify the empty state message is shown.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Sorting the proposal list

### 3.3.3 Scenario Type

Alternative_Flow

### 3.3.4 Given

I am viewing the proposal dashboard for a project with multiple proposals

### 3.3.5 When

I click on the header for a sortable column (e.g., 'Proposed Cost', 'Submission Date')

### 3.3.6 Then

The list of proposals is re-sorted in ascending order based on the selected column.

### 3.3.7 And

When I click the same header again, the list is re-sorted in descending order.

### 3.3.8 Validation Notes

Test sorting on all sortable columns (Vendor Name, Submission Date, Cost, Timeline, Status). Verify both ascending and descending sort orders are correct.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Pagination for a large number of proposals

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am viewing the proposal dashboard for a project with more proposals than the page limit (e.g., >15 proposals)

### 3.4.5 When

I view the dashboard

### 3.4.6 Then

The list displays only the first page of results.

### 3.4.7 And

I can use the pagination controls to navigate to subsequent pages of proposals.

### 3.4.8 Validation Notes

Seed a project with 20+ proposals. Verify that only the first N proposals are shown and that the pagination controls work correctly to view all proposals.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Unauthorized access attempt

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am logged in as a Vendor Contact

### 3.5.5 When

I attempt to access the URL for the proposal dashboard of a project

### 3.5.6 Then

The system prevents access and displays an appropriate authorization error message (e.g., '403 Forbidden: You do not have permission to view this page.').

### 3.5.7 Validation Notes

Attempt to access the endpoint/URL directly with a user session that does not have the System Administrator or Finance Manager role.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Data loading state

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

I am a logged-in System Administrator

### 3.6.5 When

I navigate to the proposal dashboard and the data is being fetched from the server

### 3.6.6 Then

A visual loading indicator (e.g., a spinner or skeleton screen) is displayed in place of the proposal list.

### 3.6.7 And

Once the data is successfully loaded, the indicator is replaced by the proposal list or the empty state message.

### 3.6.8 Validation Notes

Use browser developer tools to throttle the network connection and verify the loading state is visible and behaves correctly.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Page Title (e.g., 'Proposals for [Project Name]')
- Summary Count Display
- Data Table or Card List for proposals
- Table Headers (Vendor Name, Submission Date, Cost, Timeline, Status)
- Sortable Column Headers with visual indicators for sort order
- Action Button ('View Details') for each proposal row
- Pagination Controls (if applicable)
- Loading State Indicator (spinner/skeleton)
- Empty State Message

## 4.2.0 User Interactions

- User can click on column headers to sort the data.
- User can click on pagination controls to navigate between pages.
- User can click the 'View Details' button to navigate to a different view.

## 4.3.0 Display Requirements

- Financial values (Proposed Cost) must be formatted correctly for their currency (e.g., $10,000.00 USD).
- Dates (Submission Date) must be displayed in a consistent, human-readable format (e.g., YYYY-MM-DD or Month DD, YYYY).
- Proposal Status must be displayed clearly, possibly using color-coded tags for better visual scanning.

## 4.4.0 Accessibility Needs

- The data table must use correct semantic HTML (`<thead>`, `<tbody>`, `<th>` with `scope` attributes).
- All interactive elements (sort headers, buttons, pagination links) must be focusable and operable via keyboard.
- Loading states must be announced to screen readers.
- Follows WCAG 2.1 Level AA standards as per REQ-INT-001.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Only users with 'System Administrator' or 'Finance Manager' roles can view the proposal dashboard.", 'enforcement_point': 'API Gateway and Backend Service Middleware.', 'violation_handling': 'The API will return a 403 Forbidden status code. The UI will redirect to an error page.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project entity must exist to associate proposals with.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-042

#### 6.1.2.2 Dependency Reason

A project brief must be distributed to vendors before they can submit proposals.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-049

#### 6.1.3.2 Dependency Reason

The functionality for a vendor to submit a proposal must be complete, as this story depends on proposal data existing in the system.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint (e.g., GET /api/v1/projects/{projectId}/proposals) that returns a paginated and sortable list of proposals for a given project.
- A shared UI component library for tables, pagination, and loading indicators.
- The system's authentication and authorization service to enforce role-based access control.

## 6.3.0.0 Data Dependencies

- Requires access to the `Proposals` data entity, with relationships to `Vendors` and `Projects` as defined in REQ-DAT-001.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API response time for fetching the proposal list must be < 250ms (p95) as per REQ-NFR-001.
- The page's Largest Contentful Paint (LCP) must be under 2.5 seconds.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible to authorized roles (System Admin, Finance Manager).
- Data returned by the API should not expose any sensitive vendor PII beyond what is required for this view (e.g., company name, contact name).

## 7.3.0.0 Usability

- The dashboard must be intuitive, allowing an admin to quickly assess the state of proposals for a project with minimal cognitive load.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The dashboard must render and function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Low

## 8.2.0.0 Complexity Factors

- Standard CRUD-like functionality (reading a list of data).
- Requires a straightforward backend query with joins and frontend data presentation.
- Complexity is low assuming a reusable component library for UI elements like tables and pagination already exists.

## 8.3.0.0 Technical Risks

- Potential for slow database query if indexes are not properly configured on the `proposals` table (specifically on `project_id`).
- Ensuring consistent UI/UX for sorting and pagination controls across the application.

## 8.4.0.0 Integration Points

- Frontend client integrates with the backend Project Service API.
- Backend API integrates with the PostgreSQL database.
- Backend API integrates with the Authentication service for RBAC.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify dashboard displays correctly for a project with 0, 1, and N proposals.
- Verify sorting functionality for all sortable columns in both ascending and descending order.
- Verify pagination controls appear and function correctly when proposal count exceeds page size.
- Verify access is denied for unauthorized user roles (e.g., Vendor Contact).
- Verify the UI is fully responsive on desktop, tablet, and mobile viewports.

## 9.3.0.0 Test Data Needs

- A project with no proposals.
- A project with 5-10 proposals.
- A project with >15 proposals to test pagination.
- User accounts for System Admin, Finance Manager, and Vendor Contact roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe-core for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Backend and frontend code has been peer-reviewed and approved.
- Unit and integration tests are written and achieve >80% code coverage.
- E2E tests for critical paths are implemented and passing.
- UI has been reviewed by a designer or product owner for consistency and usability.
- Performance benchmarks (API response time, LCP) are met.
- Security checks (RBAC) have been validated.
- Accessibility audit (automated and manual) has been completed and passed.
- Relevant documentation (API spec) has been updated.
- Story has been successfully deployed and verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational story for the proposal management workflow. It blocks subsequent stories like proposal comparison (US-052) and status changes (US-054).
- Should be prioritized in an early sprint of the proposal management epic, immediately following the completion of vendor proposal submission (US-049).

## 11.4.0.0 Release Impact

This feature is critical for the Minimum Viable Product (MVP) as it enables the core business function of managing and selecting vendors.

