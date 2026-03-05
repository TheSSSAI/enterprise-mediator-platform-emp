# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-081 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Main Dashboard |
| As A User Story | As a System Administrator, I want to view a main d... |
| User Persona | System Administrator |
| Business Value | Provides immediate operational awareness, enabling... |
| Functional Area | Dashboards & Reporting |
| Story Theme | Business Intelligence & Oversight |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Dashboard displays all widgets with correct data

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am logged in as a System Administrator and there are active projects, pending SOWs, proposals awaiting review, and completed financial transactions in the system

### 3.1.5 When

I navigate to the main dashboard page

### 3.1.6 Then

The dashboard correctly displays widgets for 'Active Projects', 'Pending SOWs', 'Proposals Awaiting Review', 'Key Financials' (Total Revenue, Payouts, Net Profit for the last 30 days), and 'Upcoming Milestones' (next 5 milestones due in the next 7 days).

### 3.1.7 Validation Notes

Verify that the counts and financial figures on the dashboard match the actual data in the database. Check that milestone data is accurate and correctly ordered.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Dashboard widgets link to corresponding filtered views

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am viewing the main dashboard and the 'Proposals Awaiting Review' widget shows a count greater than zero

### 3.2.5 When

I click on the 'Proposals Awaiting Review' widget

### 3.2.6 Then

I am redirected to the Proposal Management page, which is pre-filtered to show only proposals with the status 'Submitted'.

### 3.2.7 Validation Notes

Test the click-through functionality for each summary widget (Active Projects, Pending SOWs, Proposals Awaiting Review) and verify the destination page is correctly filtered.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Dashboard handles empty state gracefully

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

I am logged in as a System Administrator to a new system with no data (no projects, SOWs, proposals, etc.)

### 3.3.5 When

I navigate to the main dashboard page

### 3.3.6 Then

Each widget should display '0' or a clear 'No data available' message, and the page should load without errors.

### 3.3.7 Validation Notes

Set up a test environment with a clean database. Ensure the UI is not broken and provides a good user experience for the empty state.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Dashboard data is reasonably fresh

### 3.4.3 Scenario Type

Alternative_Flow

### 3.4.4 Given

I am viewing the main dashboard

### 3.4.5 When

A new proposal is submitted by a vendor

### 3.4.6 Then

The 'Proposals Awaiting Review' count on the dashboard updates upon the next page load or refresh.

### 3.4.7 Validation Notes

The data displayed should not be stale by more than 5 minutes. This can be tested by triggering an event and immediately refreshing the dashboard.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Dashboard is accessible only to authorized roles

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am logged in as a user with a 'Client Contact' or 'Vendor Contact' role

### 3.5.5 When

I attempt to navigate directly to the admin dashboard URL

### 3.5.6 Then

I am shown an 'Access Denied' (403 Forbidden) error page or redirected to my own role-appropriate dashboard.

### 3.5.7 Validation Notes

Use test accounts for different roles to verify that the Role-Based Access Control (RBAC) for this page is correctly enforced.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A container for each dashboard widget (e.g., Card component)
- Large, clear numeric display for counts and financial figures
- Descriptive titles for each widget
- A list view within the 'Upcoming Milestones' widget
- Clickable areas/links on widgets to navigate to detailed views

## 4.2.0 User Interactions

- Clicking a widget navigates the user to a new page.
- Hovering over a widget might display a tooltip with more information (e.g., date range for financials).

## 4.3.0 Display Requirements

- Financial figures must be formatted with the correct currency symbol and decimal places.
- Dates in the 'Upcoming Milestones' widget must be in a user-friendly format (e.g., 'Jan 20, 2025').
- The layout must be responsive, adapting cleanly from a multi-column desktop view to a single-column mobile view.

## 4.4.0 Accessibility Needs

- All widgets must be keyboard navigable.
- Numeric data must be accessible to screen readers (e.g., using `aria-label` to provide context like '5 Active Projects').
- Color contrast must meet WCAG 2.1 AA standards for both light and dark themes.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Financial data on the dashboard defaults to a 'last 30 days' rolling window.

### 5.1.3 Enforcement Point

Backend API query for the dashboard.

### 5.1.4 Violation Handling

N/A - This is a display rule.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Only projects with status 'Active' are included in the 'Active Projects' count.

### 5.2.3 Enforcement Point

Backend API query for the dashboard.

### 5.2.4 Violation Handling

N/A - This is a data aggregation rule.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-029

#### 6.1.1.2 Dependency Reason

Need project creation functionality to have 'Active Projects' data.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-030

#### 6.1.2.2 Dependency Reason

Need SOW upload functionality to have 'Pending SOWs' data.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-049

#### 6.1.3.2 Dependency Reason

Need proposal submission functionality to have 'Proposals Awaiting Review' data.

### 6.1.4.0 Story Id

#### 6.1.4.1 Story Id

US-058

#### 6.1.4.2 Dependency Reason

Need client payment functionality to calculate 'Total Revenue'.

### 6.1.5.0 Story Id

#### 6.1.5.1 Story Id

US-060

#### 6.1.5.2 Dependency Reason

Need vendor payout functionality to calculate 'Total Payouts'.

## 6.2.0.0 Technical Dependencies

- A backend API endpoint to efficiently aggregate and serve dashboard data.
- Authentication service (AWS Cognito) to secure the endpoint.
- Role-Based Access Control (RBAC) middleware to enforce permissions.

## 6.3.0.0 Data Dependencies

- Read access to Projects, SOWs, Proposals, and Transactions data stores.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The dashboard API endpoint must have a p95 response time of less than 250ms (REQ-NFR-001).
- The dashboard page must achieve a Largest Contentful Paint (LCP) of under 2.5 seconds (REQ-NFR-001).

## 7.2.0.0 Security

- Access to the dashboard and its underlying API must be restricted to users with the 'System Administrator' or 'Finance Manager' role.
- All data must be fetched over HTTPS.

## 7.3.0.0 Usability

- The dashboard must provide an at-a-glance understanding of the business state with minimal cognitive load.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- Must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge (REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Backend data aggregation: The API needs to perform several efficient queries across multiple data domains (projects, financials, etc.). This may require optimization or a caching layer (e.g., Redis) to meet performance targets.
- Potential for a dedicated aggregation service that listens to events to keep dashboard stats pre-calculated, avoiding expensive real-time queries.

## 8.3.0.0 Technical Risks

- Performance of the data aggregation query at scale. A poorly designed query could become a bottleneck.
- Data consistency if a caching strategy is used. Cache invalidation logic must be robust.

## 8.4.0.0 Integration Points

- Project Service API
- Payment Service API
- User Service API (for RBAC)

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Security
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify dashboard with a fully populated database.
- Verify dashboard with an empty database.
- Verify click-through navigation from each widget.
- Verify access control for unauthorized roles.
- Verify responsive layout on mobile, tablet, and desktop.
- Verify light and dark mode rendering.

## 9.3.0.0 Test Data Needs

- A test dataset with entities in various states: projects (active, completed), SOWs (pending, processed), proposals (submitted, accepted), and transactions (payments, payouts).
- Test user accounts for System Admin, Finance Manager, Client Contact, and Vendor Contact roles.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A load testing tool (e.g., k6, JMeter) for the API endpoint.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit and integration tests implemented with >80% coverage and passing
- E2E tests for all key scenarios are implemented and passing
- User interface reviewed and approved by Product Owner/UX designer
- Performance requirements (API latency, LCP) verified and met
- Security requirements (RBAC) validated
- Online help documentation for the dashboard page is created/updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a high-value feature for the primary user persona but depends on several core functionalities. It should be scheduled in a sprint immediately following the completion of its prerequisite stories.
- The backend API work and frontend UI work can potentially be parallelized.

## 11.4.0.0 Release Impact

This is a key feature for the initial release (MVP) as it provides the primary landing and monitoring page for administrators.

