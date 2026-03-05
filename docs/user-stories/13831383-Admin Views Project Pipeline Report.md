# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-085 |
| Elaboration Date | 2025-01-26 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Project Pipeline Report |
| As A User Story | As a System Administrator, I want to view a visual... |
| User Persona | System Administrator. This role requires a high-le... |
| Business Value | Provides critical operational intelligence by visu... |
| Functional Area | Business Intelligence & Reporting |
| Story Theme | Operational Oversight |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Report Visualization and Structure

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator and I navigate to the 'Reports' section

### 3.1.5 When

I select the 'Project Pipeline Report'

### 3.1.6 Then

The system displays a Kanban-style board with a distinct column for each project status: 'Pending', 'Proposed', 'Awarded', 'Active', 'In Review', 'Completed', 'On Hold', 'Cancelled', and 'Disputed'.

### 3.1.7 Validation Notes

Verify that all nine status columns are rendered correctly and in a logical order reflecting the project lifecycle.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Project Count Display

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing the Project Pipeline Report and there are 5 projects in 'Active' status

### 3.2.5 When

The report finishes loading

### 3.2.6 Then

The header of the 'Active' column displays the status name and the count of projects, formatted as 'Active (5)'.

### 3.2.7 Validation Notes

Check this for multiple statuses with varying counts, including zero.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Project Card Information and Navigation

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

A project named 'Q1 Marketing Campaign' for 'Client Corp' with a value of $50,000 is in the 'Awarded' status

### 3.3.5 When

I view the 'Awarded' column in the pipeline report

### 3.3.6 Then

I see a card representing the project that displays at a minimum: 'Q1 Marketing Campaign', 'Client Corp', and '$50,000'.

### 3.3.7 Validation Notes

Clicking this card must navigate the user to the detailed workspace for the 'Q1 Marketing Campaign' project in the same browser tab.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Filtering by Client

### 3.4.3 Scenario Type

Happy_Path

### 3.4.4 Given

I am viewing the Project Pipeline Report showing projects from multiple clients

### 3.4.5 When

I select 'Client Corp' from a client filter dropdown and apply the filter

### 3.4.6 Then

The report view updates to show only projects associated with 'Client Corp', and all column counts are recalculated to reflect this filtered view.

### 3.4.7 Validation Notes

Test with a client that has projects in multiple statuses and a client with no projects.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Filtering by Date Range

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

I am viewing the Project Pipeline Report

### 3.5.5 When

I select a 'Project Creation Date' range from 'Last 30 Days' and apply the filter

### 3.5.6 Then

The report view updates to show only projects created within the last 30 days, and all column counts are recalculated.

### 3.5.7 Validation Notes

The default view should be for projects created in the last 90 days. Test with various predefined ranges (e.g., This Quarter, This Year) and a custom date range picker.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Empty State for No Matching Projects

### 3.6.3 Scenario Type

Edge_Case

### 3.6.4 Given

I am viewing the Project Pipeline Report

### 3.6.5 When

I apply filters that result in no matching projects

### 3.6.6 Then

The Kanban board area displays a user-friendly message, such as 'No projects found for the selected criteria', instead of an empty or broken interface.

### 3.6.7 Validation Notes

Verify that the column headers are still visible but the content area shows the message.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Empty Status Column

### 3.7.3 Scenario Type

Edge_Case

### 3.7.4 Given

There are no projects currently in the 'Disputed' status

### 3.7.5 When

I view the Project Pipeline Report

### 3.7.6 Then

The 'Disputed' column is still displayed in the report, with its header showing 'Disputed (0)'.

### 3.7.7 Validation Notes

This ensures the UI remains consistent and the user is aware of all possible statuses, even if none are currently occupied.

## 3.8.0 Criteria Id

### 3.8.1 Criteria Id

AC-008

### 3.8.2 Scenario

Invalid Date Range Filter

### 3.8.3 Scenario Type

Error_Condition

### 3.8.4 Given

I am using the custom date range filter on the report

### 3.8.5 When

I select a 'Start Date' that is after the 'End Date' and attempt to apply the filter

### 3.8.6 Then

A validation error message is displayed near the date picker, and the report data is not refreshed.

### 3.8.7 Validation Notes

The 'Apply' button should be disabled until a valid date range is selected.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Page Title: 'Project Pipeline Report'
- Filter controls: Dropdown for Client, Date Range Picker (with presets like 'Last 30/90 Days', 'This Quarter')
- Kanban Board: A container for the status columns
- Status Columns: One for each project status, with a header showing title and count
- Project Cards: Draggable-looking (though not functional yet) cards with Project Name, Client Name, and Project Value
- Empty State Message Area
- Loading Indicator (e.g., skeleton screen or spinner)

## 4.2.0 User Interactions

- User can select a client from a typeahead/searchable dropdown.
- User can select a predefined date range or a custom range.
- Applying filters updates the report view without a full page reload.
- Clicking a project card navigates to the project's detail page.
- On smaller viewports, the Kanban board should become horizontally scrollable to maintain usability.

## 4.3.0 Display Requirements

- Project statuses must be displayed in a logical order that reflects the typical project flow.
- Financial values on project cards must be formatted according to the system's base currency settings.
- Counts in column headers must be clearly legible.

## 4.4.0 Accessibility Needs

- The report must be compliant with WCAG 2.1 Level AA standards.
- All interactive elements (filters, cards) must be keyboard-navigable and have appropriate focus states.
- Column headers and project card details must be accessible to screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Access to the Project Pipeline Report is restricted to users with the 'System Administrator' or 'Finance Manager' role.

### 5.1.3 Enforcement Point

API Gateway and Backend Service Middleware.

### 5.1.4 Violation Handling

An attempt to access the report URL or API endpoint by an unauthorized user will result in a 403 Forbidden error.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

The list of project statuses displayed as columns must be sourced from the system's master list of project statuses to ensure consistency.

### 5.2.3 Enforcement Point

Backend service logic that generates the report.

### 5.2.4 Violation Handling

If a project has an invalid or unrecognized status, it should be flagged in system logs and potentially excluded from the report to prevent errors.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

A project must be creatable to be displayed in the report.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-041

#### 6.1.2.2 Dependency Reason

The ability to change a project's status is required for the pipeline report to reflect a dynamic workflow.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-012

#### 6.1.3.2 Dependency Reason

Client profiles must exist to be associated with projects and used in filtering.

## 6.2.0.0 Technical Dependencies

- A performant database indexing strategy on the 'projects' table, specifically on 'status', 'client_id', and 'created_at' columns.
- Availability of the UI component library (Radix UI, Tailwind CSS) for building the Kanban view.

## 6.3.0.0 Data Dependencies

- Access to the complete 'Projects' and 'Clients' datasets.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The report API endpoint must respond in under 500ms for a dataset of up to 10,000 projects.
- The frontend report page should achieve a Largest Contentful Paint (LCP) of under 2.5 seconds, as per REQ-NFR-001.

## 7.2.0.0 Security

- Access to the report's API endpoint must be protected by the system's authentication and authorization mechanism (JWT, RBAC).
- All filter parameters must be sanitized on the backend to prevent SQL injection or other injection attacks.

## 7.3.0.0 Usability

- The report should be intuitive and require no training for a System Admin to understand.
- Filter interactions should feel responsive and provide immediate feedback.

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards as defined in REQ-INT-001.

## 7.5.0.0 Compatibility

- The report must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge, as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend: Crafting a single, efficient database query that handles aggregation and dynamic filtering without performance degradation.
- Frontend: Building a responsive Kanban layout that works well on both large desktop screens and smaller tablet viewports.
- State Management: Managing filter state and triggering data refetches on the client-side can be complex.

## 8.3.0.0 Technical Risks

- Performance risk if the database query is not properly optimized, leading to slow load times as the number of projects grows.
- UI complexity risk in making the Kanban board fully responsive and accessible.

## 8.4.0.0 Integration Points

- Integrates with the Project Service to fetch project data.
- Integrates with the User Service (via API Gateway) to verify user role and permissions.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify report accuracy with a known set of project data.
- Test all filter combinations (client only, date only, client and date).
- Test responsive layout on various screen sizes (desktop, tablet, mobile).
- Test navigation from a project card to the project detail page.
- Test performance with a large dataset (e.g., 10,000+ projects).

## 9.3.0.0 Test Data Needs

- A seeded database with at least 50 projects distributed across all statuses.
- At least 10 different clients, with some clients having multiple projects.
- Projects with creation dates spanning at least one year to test date filters effectively.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Storybook for UI component isolation and testing.
- Axe for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E tests for the happy path and filtering functionality are passing
- User interface reviewed for responsiveness and adherence to design specifications
- Performance testing confirms API response times are within the defined limits
- Automated accessibility scans pass with no critical violations
- User documentation for the 'Project Pipeline Report' is created or updated
- Story deployed and verified in the staging environment by a QA engineer or product owner

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story provides significant value for operational visibility and should be prioritized soon after the core project management features are stable.
- Requires both frontend and backend development effort that can be parallelized.

## 11.4.0.0 Release Impact

- This is a key feature for the 'Business Intelligence' module and a major value-add for administrative users.

