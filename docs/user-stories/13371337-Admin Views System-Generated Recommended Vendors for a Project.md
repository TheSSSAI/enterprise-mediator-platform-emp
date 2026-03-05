# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-039 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views System-Generated Recommended Vendors f... |
| As A User Story | As a System Administrator managing a new project, ... |
| User Persona | System Administrator |
| Business Value | Automates and improves the quality of the vendor s... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing and Vendor Matchmaking |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Happy Path: Display ranked list of recommended vendors

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a System Admin logged in and viewing a project workspace where the Project Brief has been approved

### 3.1.5 When

I navigate to the 'Recommended Vendors' section of the project

### 3.1.6 Then

The system displays a list of vendors ranked in descending order of their similarity score to the Project Brief.

### 3.1.7 Validation Notes

Verify the list is sorted by the score. The score should be calculated via a vector similarity search between the Project Brief's embedding and the vendors' skill embeddings.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Vendor information in the recommendation list

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'Recommended Vendors' list is displayed

### 3.2.5 When

I view an entry in the list

### 3.2.6 Then

I must see the Vendor's Company Name, the calculated Similarity Score (e.g., '92% Match'), and a list of the top matching skills/tags.

### 3.2.7 Validation Notes

Check that all three data points are present and accurate for each vendor in the list.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Only active vendors are recommended

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

There are vendors in the system with statuses of 'Active', 'Pending Vetting', and 'Deactivated' who would otherwise match the Project Brief

### 3.3.5 When

The recommendation list is generated

### 3.3.6 Then

Only vendors with the status 'Active' appear in the list.

### 3.3.7 Validation Notes

Create test data with vendors in various states and confirm only the 'Active' ones are returned by the recommendation query.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Edge Case: No suitable vendors found

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am viewing a project with an approved Project Brief for a highly niche skill set that no active vendor possesses

### 3.4.5 When

I navigate to the 'Recommended Vendors' section

### 3.4.6 Then

The system displays a clear, user-friendly message such as 'No suitable vendors found. Consider broadening the project scope or adding vendors with the required skills.'

### 3.4.7 Validation Notes

Test with a project brief whose vector has no close neighbors in the vendor vector space.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Error Condition: Recommendation service fails

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am a System Admin viewing a project

### 3.5.5 And

The backend service responsible for vector search is unavailable or returns an error

### 3.5.6 When

I attempt to view the 'Recommended Vendors' list

### 3.5.7 Then

The UI displays a non-technical error message like 'Could not retrieve vendor recommendations at this time. Please try again later.' and a correlation ID is logged for support.

### 3.5.8 Validation Notes

Use a tool like Mock-Server to simulate a 500 error from the search service API and verify the frontend handles it gracefully.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

UI loading state

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I navigate to the 'Recommended Vendors' section

### 3.6.5 When

The system is fetching the list of recommendations from the backend

### 3.6.6 Then

A loading indicator (e.g., a spinner or skeleton screen) is displayed until the data is loaded or an error occurs.

### 3.6.7 Validation Notes

Throttle network speed in browser developer tools to visually confirm the loading state appears.

## 3.7.0 Criteria Id

### 3.7.1 Criteria Id

AC-007

### 3.7.2 Scenario

Project Brief not yet approved

### 3.7.3 Scenario Type

Alternative_Flow

### 3.7.4 Given

I am viewing a project workspace where the Project Brief has not yet been approved

### 3.7.5 When

I navigate to the 'Recommended Vendors' section

### 3.7.6 Then

The section is disabled or displays a message indicating that the Project Brief must be approved first to generate recommendations.

### 3.7.7 Validation Notes

Verify this state for a newly created project before the brief is approved.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A tab or dedicated section within the project workspace labeled 'Recommended Vendors'.
- A list/table component to display vendor cards or rows.
- For each vendor: Company Name, Similarity Score (visualized as a percentage or progress bar), and a list of skill tags.
- Checkboxes next to each vendor to allow for selection (for use in US-042).
- A message area for displaying 'no results' or error messages.
- A loading state indicator (e.g., skeleton loader).

## 4.2.0 User Interactions

- The list loads automatically upon navigating to the section.
- Clicking on a vendor's name navigates the admin to that vendor's detailed profile page.
- The admin can select one or more vendors using the checkboxes.

## 4.3.0 Display Requirements

- The list must be clearly ranked with the highest score at the top.
- The similarity score should be prominent and easy to understand.

## 4.4.0 Accessibility Needs

- The vendor list must be keyboard navigable (up/down arrows, tab).
- Screen readers must announce all key information for each vendor entry, including the similarity score.
- UI elements must have sufficient color contrast per WCAG 2.1 AA standards.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Only vendors with a status of 'Active' are eligible for project recommendations.

### 5.1.3 Enforcement Point

Backend API query for vendor matching.

### 5.1.4 Violation Handling

Vendors with any other status are filtered out and not included in the result set.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Vendor recommendations can only be generated after a project's 'Project Brief' has been formally approved by an Admin.

### 5.2.3 Enforcement Point

API endpoint and UI component.

### 5.2.4 Violation Handling

The API will return an error if recommendations are requested for a project without an approved brief. The UI will be disabled.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-026

#### 6.1.1.2 Dependency Reason

The system needs a pool of 'Active' vendors to recommend from.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-028

#### 6.1.2.2 Dependency Reason

Vendor profiles must be populated with skills/tags to generate the vector embeddings used for matching.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-036

#### 6.1.3.2 Dependency Reason

The approved 'Project Brief' provides the source data (skills, scope) from which the project's vector embedding is generated. This is the direct trigger for this story's functionality.

## 6.2.0.0 Technical Dependencies

- PostgreSQL database with the `pgvector` extension enabled and configured.
- A configured AI model (e.g., via OpenAI API) for generating text embeddings.
- An asynchronous eventing system (e.g., AWS SNS/SQS) to trigger the recommendation generation after brief approval.

## 6.3.0.0 Data Dependencies

- Requires existing vendor profiles with populated 'Areas of Expertise / Skills' fields.
- Requires a project entity with an associated, processed, and approved 'Project Brief'.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The p95 latency for the API call to fetch recommendations must be under 500ms.
- The UI must render the list within 2.5 seconds (LCP) from the user's navigation action.

## 7.2.0.0 Security

- The API endpoint must be protected and only accessible to authenticated users with the 'System Administrator' role.
- The query must be parameterized to prevent SQL injection, even though the input is system-generated.

## 7.3.0.0 Usability

- The recommendation list should be easy to scan and interpret, allowing the admin to quickly grasp the top candidates.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- Functionality must be consistent across the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires implementation of vector similarity search using `pgvector`, which may be a new technology for the team.
- The quality of recommendations is highly dependent on the quality of the text embeddings, which may require tuning.
- Managing the asynchronous data flow: Brief Approval -> Event -> Embedding Generation -> Ready for Query.
- Backend logic to combine vector search results with other vendor data (name, status, etc.) efficiently.

## 8.3.0.0 Technical Risks

- The chosen embedding model may not produce effective similarity scores for the specific domain of consulting skills, requiring experimentation.
- Poorly optimized vector search queries could lead to performance degradation as the number of vendors grows.

## 8.4.0.0 Integration Points

- Project Service: Triggers the recommendation process upon brief approval.
- Search Service (or a dedicated recommendation service): Contains the logic to perform the vector search against the vendor database.
- Vendor Service: Provides the vendor data (profiles, skills, status) and their pre-calculated vector embeddings.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Performance
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify a known high-match vendor appears at the top of the list.
- Verify a known low-match vendor appears at the bottom or not at all.
- Verify a deactivated vendor does not appear in the list, even with a perfect skill match.
- Verify the UI's behavior when the API returns an empty list.
- Verify the UI's behavior when the API returns a 5xx error.

## 9.3.0.0 Test Data Needs

- A set of at least 20 vendor profiles with diverse skills and statuses ('Active', 'Deactivated').
- A set of 5-10 pre-approved Project Briefs with varying skill requirements to test the ranking logic.
- Vector embeddings for all test vendors and project briefs must be pre-generated in the test database.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for E2E tests.
- A database seeding script to populate test data.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by at least one other developer
- Unit and integration tests implemented with >80% code coverage for new logic
- E2E tests for the happy path and key error conditions are passing
- User interface reviewed for usability and adherence to design specifications
- Performance of the recommendation API call is measured and meets requirements
- Accessibility of the UI component is verified using automated tools and manual keyboard testing
- API documentation (OpenAPI) is updated for the new endpoint
- Story deployed and verified in the staging environment with a QA sign-off

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

8

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a core feature of the AI workflow and a dependency for subsequent stories (US-042).
- Requires backend and frontend collaboration.
- The team should allocate time for potential R&D on `pgvector` query optimization if it's a new technology for them.

## 11.4.0.0 Release Impact

This story is critical for the initial product launch as it delivers on the key 'intelligent matchmaking' value proposition.

