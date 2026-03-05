# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-040 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Views Vendor Similarity Score on Recommendat... |
| As A User Story | As a System Administrator reviewing potential vend... |
| User Persona | System Administrator responsible for matching proj... |
| Business Value | Improves the efficiency and accuracy of the vendor... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Processing and Vendor Matching |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Display of Similarity Score

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a System Admin is viewing the 'Recommended Vendors' list for a project where recommendations have been successfully generated

### 3.1.5 When

the page loads and displays the list of vendors

### 3.1.6 Then

each vendor entry in the list must display a similarity score, formatted as a percentage (e.g., '92%').

### 3.1.7 Validation Notes

Verify that the API response for recommended vendors includes a 'similarityScore' field and the UI correctly renders this value as a percentage for every vendor in the list.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Default Sorting by Score

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the 'Recommended Vendors' list is displayed

### 3.2.5 When

the page initially loads

### 3.2.6 Then

the vendors must be sorted in descending order based on their similarity score.

### 3.2.7 Validation Notes

Check the order of vendors on the UI. The vendor with the highest score should be at the top. Use test data with varied scores to confirm.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Informative Tooltip on Hover

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the 'Recommended Vendors' list is displayed with similarity scores

### 3.3.5 When

the user hovers their mouse cursor over a vendor's similarity score or its associated visual indicator

### 3.3.6 Then

a tooltip appears providing a brief explanation, such as 'Similarity based on skills, technologies, and scope analysis.'

### 3.3.7 Validation Notes

Manually hover over the score element to ensure the tooltip appears and contains the correct explanatory text. Verify this works for all vendors.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Secondary Sorting for Tied Scores

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

the 'Recommended Vendors' list contains at least two vendors with the exact same similarity score

### 3.4.5 When

the list is displayed with the default sort order

### 3.4.6 Then

the vendors with the tied score are sorted alphabetically by company name as a secondary criterion.

### 3.4.7 Validation Notes

Requires test data where two vendors have identical scores. Verify their secondary sort order is alphabetical.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Visual Representation of Score

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

the similarity score is displayed for a vendor

### 3.5.5 When

the UI renders the vendor entry

### 3.5.6 Then

the percentage text is accompanied by a visual element (e.g., a radial progress bar or a filled horizontal bar) that graphically represents the score.

### 3.5.7 Validation Notes

Visually inspect the UI to confirm the presence and correctness of the graphical indicator. A score of 75% should show the indicator as 75% complete.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Accessibility for Screen Readers

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

a user is navigating the 'Recommended Vendors' list using a screen reader

### 3.6.5 When

the screen reader's focus moves to a vendor entry

### 3.6.6 Then

the screen reader announces the vendor's name and their similarity score clearly, for example: 'Vendor ABC, 92 percent match'.

### 3.6.7 Validation Notes

Use a screen reader (e.g., NVDA, VoiceOver) to navigate the list and confirm the output. Ensure the score and its meaning are conveyed.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A text element to display the percentage score (e.g., '92%').
- A visual indicator (e.g., radial progress bar) next to the text score.
- A tooltip component that appears on hover/focus of the score element.

## 4.2.0 User Interactions

- The list of vendors is automatically sorted by score on load.
- Hovering over the score reveals an explanatory tooltip.

## 4.3.0 Display Requirements

- The similarity score must be clearly associated with its respective vendor.
- The score should be visually prominent enough to be easily scanned but not so large that it disrupts the layout.

## 4.4.0 Accessibility Needs

- The score's text and visual indicator must meet WCAG 2.1 AA contrast ratio requirements.
- The score must be readable by screen readers, with appropriate ARIA attributes if necessary.
- The tooltip must be accessible via keyboard focus.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': 'The similarity score is a read-only, system-generated value and cannot be manually edited by any user.', 'enforcement_point': 'User Interface and API Layer', 'violation_handling': 'No UI element for editing the score will be presented. Any API attempt to modify the score will be rejected.'}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-039

#### 6.1.1.2 Dependency Reason

This story adds the similarity score to the recommended vendor list view, which is created in US-039. The backend logic to generate the list of vendors must be complete before their scores can be displayed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-036

#### 6.1.2.2 Dependency Reason

The approved Project Brief, finalized in US-036, is the source data used to generate vector embeddings for matching. This must be completed to have something to match against.

## 6.2.0.0 Technical Dependencies

- The backend API endpoint for fetching recommended vendors must be updated to include the similarity score in the response payload for each vendor.
- The PostgreSQL database must have the `pgvector` extension enabled and functioning.
- The AI Ingestion Service must be capable of generating vector embeddings for project briefs.

## 6.3.0.0 Data Dependencies

- A corpus of vendor profiles with pre-generated vector embeddings for their skills and expertise must be available in the database.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- Displaying the similarity scores must not introduce more than 50ms of additional latency to the rendering of the recommended vendors list.
- The score calculation itself is an asynchronous process and is not part of this story's performance requirement, but the retrieval of the pre-calculated score must be fast.

## 7.2.0.0 Security

- The similarity score is considered internal, sensitive data and should only be visible to authenticated internal users (System Admin, Finance Manager) with permission to view the project.

## 7.3.0.0 Usability

- The score and its visual representation should be immediately understandable to a non-technical user.
- The tooltip should provide just enough context without overwhelming the user with technical jargon.

## 7.4.0.0 Accessibility

- Must comply with WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must render and function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Frontend: Implementation of a custom, accessible, and visually appealing component for the score (text + graphic + tooltip).
- Backend: Minor modification to an existing API endpoint and its data transfer object (DTO) to include the score.
- Integration: Ensuring the frontend and backend communicate the new data point correctly.

## 8.3.0.0 Technical Risks

- The chosen UI component for the visual indicator may have accessibility issues that require custom workarounds.
- The similarity score calculation (handled by a dependency) might produce non-normalized values, requiring the API to normalize them to a 0-100 scale before sending to the client.

## 8.4.0.0 Integration Points

- The Project Service API, specifically the endpoint that returns recommended vendors for a project.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify scores display correctly for a list of vendors.
- Verify default sorting is by score descending.
- Verify secondary sorting (alphabetical) for tied scores.
- Verify tooltip functionality on hover and keyboard focus.
- Verify UI responsiveness on different screen sizes.
- Verify screen reader output for the vendor list entries.

## 9.3.0.0 Test Data Needs

- A project with a processed SOW.
- A set of at least 5 vendors with profiles that will result in a range of similarity scores (e.g., high, medium, low).
- A specific test case with two vendors having identical scores to test secondary sorting.

## 9.4.0.0 Testing Tools

- Jest/React Testing Library for frontend unit tests.
- Playwright for E2E tests.
- Axe or Lighthouse for automated accessibility checks.
- NVDA or VoiceOver for manual screen reader testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented and passing with >80% coverage for new components
- Integration testing completed successfully
- User interface reviewed and approved for aesthetics and usability
- Performance requirements verified
- Security requirements validated
- Accessibility audit passed (automated and manual)
- Documentation for the API change is updated in the OpenAPI specification
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

3

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is dependent on the completion of US-039. It should be scheduled in the same or a subsequent sprint.
- Requires collaboration between a frontend and a backend developer to coordinate the API change and UI implementation.

## 11.4.0.0 Release Impact

- This is a key feature for the AI-powered matching workflow and is critical for demonstrating the value of the system. It should be included in the initial release of the feature.

