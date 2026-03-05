# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-052 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Compares Proposals in a Side-by-Side View |
| As A User Story | As a System Administrator, I want to view multiple... |
| User Persona | System Administrator responsible for vendor select... |
| Business Value | Improves decision quality, increases operational e... |
| Functional Area | Project Lifecycle Management |
| Story Theme | Proposal and Award Workflow |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Compare multiple selected proposals

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a System Admin is viewing the list of proposals for a project, and at least two proposals have been submitted

### 3.1.5 When

the admin selects two or more proposals using checkboxes and clicks the 'Compare Selected' button

### 3.1.6 Then

the system navigates to a dedicated comparison view, displaying the selected proposals in a side-by-side layout (e.g., columns in a table).

### 3.1.7 Validation Notes

Verify that the view loads and the correct number of proposals are displayed as columns.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Correct data points are displayed for comparison

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the proposal comparison view is displayed

### 3.2.5 When

the admin examines the content

### 3.2.6 Then

the view must contain rows for 'Vendor Name', 'Cost', 'Timeline', 'Key Personnel', 'Vendor On-Time Completion Rate', and 'Vendor Average Project Value'.

### 3.2.7 Validation Notes

Check that all specified data fields are present as rows and populated with data from the corresponding proposal/vendor.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Handling of missing or null data

### 3.3.3 Scenario Type

Edge_Case

### 3.3.4 Given

the proposal comparison view is displayed for a proposal from a new vendor with no performance history or an incomplete proposal submission

### 3.3.5 When

a data point is not available (e.g., 'On-Time Completion Rate' is null or 'Key Personnel' was not submitted)

### 3.3.6 Then

the corresponding cell in the comparison view displays a clear indicator such as 'N/A' or 'Not Provided'.

### 3.3.7 Validation Notes

Test with a vendor that has null values for performance metrics and a proposal with an empty 'Key Personnel' field.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Compare button state based on selection

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

the System Admin is on the proposal list view

### 3.4.5 When

fewer than two proposals are selected

### 3.4.6 Then

the 'Compare Selected' button is disabled or hidden.

### 3.4.7 Validation Notes

Verify the button is disabled with 0 and 1 proposal selected, and becomes enabled when 2 or more are selected.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

UI responsiveness for different screen sizes

### 3.5.3 Scenario Type

Alternative_Flow

### 3.5.4 Given

the proposal comparison view is open with three or more proposals

### 3.5.5 When

the browser window is resized to a tablet or mobile viewport

### 3.5.6 Then

the layout adjusts to maintain readability, such as stacking proposal cards vertically or enabling horizontal scrolling within the comparison table.

### 3.5.7 Validation Notes

Use browser developer tools to test the view at various common breakpoints (e.g., 375px, 768px, 1024px).

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Access to full proposal details

### 3.6.3 Scenario Type

Happy_Path

### 3.6.4 Given

the proposal comparison view is displayed

### 3.6.5 When

the admin needs more detail on a specific proposal

### 3.6.6 Then

each proposal column contains a clear link or button (e.g., 'View Full Proposal') that navigates to the detailed view of that proposal or opens its submitted documents.

### 3.6.7 Validation Notes

Click the link for each proposal in the comparison view and verify it leads to the correct detailed proposal page.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- Checkboxes on the proposal list to select proposals for comparison.
- A 'Compare Selected' button on the proposal list page.
- A comparison view (table or grid layout) with proposals as columns and data points as rows.
- Clear headings for each column (Vendor Name) and row (Cost, Timeline, etc.).
- Links within each proposal column to 'View Full Proposal'.
- A 'Back' or breadcrumb navigation element to return to the project's proposal list.

## 4.2.0 User Interactions

- User selects/deselects proposals via checkboxes.
- User clicks a button to initiate the comparison.
- On smaller screens, the user may need to scroll horizontally to see all proposals or scroll vertically through stacked cards.

## 4.3.0 Display Requirements

- Financial data (Cost) must be formatted with currency symbols and appropriate decimal places.
- Performance metrics should be clearly labeled (e.g., '%', '$').
- The currently viewed project's name should be visible on the comparison screen for context.

## 4.4.0 Accessibility Needs

- The comparison table must use proper `<table>`, `<thead>`, `<tbody>`, `<th>`, and `<td>` elements with `scope` attributes for screen reader compatibility.
- All interactive elements (buttons, links) must be keyboard-focusable and have clear focus indicators.
- Sufficient color contrast must be maintained for all text and UI elements, adhering to WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "Only proposals in a 'Submitted' or 'In Review' status can be selected for comparison.", 'enforcement_point': 'Frontend UI on the proposal list page.', 'violation_handling': "Proposals with other statuses (e.g., 'Rejected', 'Accepted') will have their selection checkbox disabled."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-051

#### 6.1.1.2 Dependency Reason

The comparison action is initiated from the proposal dashboard view, which must exist first.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-049

#### 6.1.2.2 Dependency Reason

Requires the existence of submitted proposal data to compare.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-024

#### 6.1.3.2 Dependency Reason

Requires the vendor profile data model and API to fetch vendor-specific performance metrics.

## 6.2.0.0 Technical Dependencies

- Backend API endpoint capable of fetching and aggregating data for multiple specified proposal IDs and their associated vendor metrics in a single request.

## 6.3.0.0 Data Dependencies

- Access to structured proposal data (cost, timeline, etc.).
- Access to vendor profile data (performance metrics).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The API call to fetch data for the comparison view must have a 95th percentile response time of less than 250ms (as per REQ-NFR-001).
- The comparison page's Largest Contentful Paint (LCP) should be under 2.5 seconds.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible by authenticated users with the 'System Administrator' role (as per REQ-SEC-001).

## 7.3.0.0 Usability

- The comparison layout should be intuitive, presenting the most critical data points prominently to facilitate quick decision-making.

## 7.4.0.0 Accessibility

- The feature must comply with Web Content Accessibility Guidelines (WCAG) 2.1 Level AA standards (as per REQ-INT-001).

## 7.5.0.0 Compatibility

- The feature must render correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge (as per REQ-DEP-001).

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Frontend: Building a responsive and accessible data grid/table for comparison can be complex, especially handling horizontal overflow on small screens.
- Backend: Designing an efficient database query to aggregate data from `proposals`, `vendors`, and potentially other tables without performance bottlenecks (e.g., N+1 problem).

## 8.3.0.0 Technical Risks

- Performance degradation of the data aggregation API call if not optimized correctly, especially as the number of proposals per project grows.
- UI complexity in presenting a large number of proposals (e.g., 10+) in a clean, comparable format on smaller viewports.

## 8.4.0.0 Integration Points

- Frontend integrates with a new backend API endpoint for fetching comparison data.
- Backend service integrates with the PostgreSQL database, querying multiple tables.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility
- Performance

## 9.2.0.0 Test Scenarios

- Compare 2 proposals.
- Compare 5+ proposals to test UI scaling.
- Compare proposals where one has missing data.
- Verify button states with 0, 1, and 2+ selections.
- End-to-end test from login to viewing the comparison page.
- Test responsiveness across mobile, tablet, and desktop breakpoints.

## 9.3.0.0 Test Data Needs

- A test project with at least 5 submitted proposals from different vendors.
- At least one vendor with no historical performance data.
- At least one proposal submitted with optional fields left blank.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- Axe for automated accessibility checks.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >= 80% coverage and passing
- Integration testing for the new API endpoint completed successfully
- E2E tests covering the primary user flow are passing
- User interface reviewed for responsiveness and approved by UX/Product
- Performance requirements verified against benchmarks
- Accessibility audit (automated and manual) passed
- API documentation (OpenAPI) is updated
- Story deployed and verified in the staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- Ensure prerequisite stories (US-051, US-049, US-024) are completed in a prior sprint or early in the same sprint.
- The backend API task should be completed before the frontend task can be fully integrated and tested.

## 11.4.0.0 Release Impact

This is a key feature for the proposal evaluation workflow and is critical for achieving the core business value of the platform.

