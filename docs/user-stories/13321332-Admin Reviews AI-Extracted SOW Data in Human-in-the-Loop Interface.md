# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-034 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | Admin Reviews AI-Extracted SOW Data in Human-in-th... |
| As A User Story | As a System Administrator, I want to review the da... |
| User Persona | System Administrator responsible for project data ... |
| Business Value | Ensures the accuracy of project data before it's u... |
| Functional Area | Project Lifecycle Management |
| Story Theme | AI-Powered SOW Automation |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

Successful Display of Extracted Data

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in System Administrator and I am on the project workspace page for a project whose SOW has a status of 'Processed'

### 3.1.5 When

I navigate to the 'SOW Review' tab or section

### 3.1.6 Then

The system must display a 'human-in-the-loop' interface with editable fields for 'Required Skills', 'Technologies', 'Scope Summary', 'Deliverables', and 'Timeline', all pre-populated with the data extracted by the AI.

### 3.1.7 Validation Notes

Verify that the data displayed in the UI fields matches the data stored in the database from the AI processing job.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

Side-by-Side View for Verification

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

The 'SOW Review' interface is loaded with AI-extracted data

### 3.2.5 When

I am viewing the interface

### 3.2.6 Then

The system must display a non-editable, scrollable view of the sanitized SOW text adjacent to the editable data fields, allowing for easy cross-referencing and verification.

### 3.2.7 Validation Notes

Confirm the sanitized text is rendered correctly and that it is read-only.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

Handling SOW Processing Failure

### 3.3.3 Scenario Type

Error_Condition

### 3.3.4 Given

I am a logged-in System Administrator and I navigate to a project whose SOW has a status of 'Failed'

### 3.3.5 When

I attempt to view the 'SOW Review' interface

### 3.3.6 Then

The system must display a clear error message indicating that the SOW processing failed, including the error summary and correlation ID.

### 3.3.7 And

The data extraction form must not be displayed, but an option to 'Retry Processing' should be available.

### 3.3.8 Validation Notes

Create a test case where the SOW processing job is forced to fail and verify the UI response.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

Handling Partially Extracted Data

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

The AI processing was only able to extract data for some, but not all, of the required fields

### 3.4.5 When

I load the 'SOW Review' interface

### 3.4.6 Then

The interface must load successfully, with the populated fields showing the extracted data and the empty fields being clearly identifiable (e.g., empty with placeholder text like 'No data extracted').

### 3.4.7 Validation Notes

Use a test SOW that is known to result in partial extraction and verify the UI handles it gracefully.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Presence of Action Controls

### 3.5.3 Scenario Type

Happy_Path

### 3.5.4 Given

The 'SOW Review' interface is loaded

### 3.5.5 When

I am viewing the interface

### 3.5.6 Then

The interface must include 'Save Changes' and 'Approve Brief' buttons to allow for subsequent actions.

### 3.5.7 Validation Notes

Verify the buttons are present. Their functionality will be implemented in US-035 and US-036, but their existence is part of this story's UI.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Navigating Away with Unsaved Changes

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

I have modified data in one of the extracted fields but have not clicked 'Save Changes'

### 3.6.5 When

I attempt to navigate to a different page or close the tab

### 3.6.6 Then

The system must display a confirmation modal with the message 'You have unsaved changes. Are you sure you want to leave?' and provide options to 'Leave Page' or 'Stay on Page'.

### 3.6.7 Validation Notes

This requires client-side state management to track if the form data is 'dirty'.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated page or tab titled 'SOW Review & Brief Creation'.
- Tag-based input component for 'Required Skills' and 'Technologies'.
- Rich text editor (WYSIWYG) for 'Scope Summary'.
- A dynamic list component for 'Deliverables' allowing items to be added, edited, and removed.
- Text input or date-picker components for 'Timeline'.
- A read-only, scrollable panel to display the full sanitized SOW text.
- Primary button: 'Approve Brief'.
- Secondary button: 'Save Changes'.
- Tertiary action/button: 'Retry Processing' (only visible on failure).

## 4.2.0 User Interactions

- The 'Save Changes' button should be disabled until a change is made to the form data.
- The layout must be responsive, ensuring the side-by-side view adapts gracefully on smaller screens (e.g., stacking vertically).
- Tooltips should be available for each field explaining what data is expected.

## 4.3.0 Display Requirements

- The current status of the SOW (e.g., 'Processed - Pending Review') must be clearly visible on the page.
- Extracted data must be formatted for readability (e.g., skills as distinct tags, deliverables as a bulleted list).

## 4.4.0 Accessibility Needs

- All form fields must have `aria-label` attributes and be associated with a visible `<label>`.
- The interface must be fully navigable and operable using only a keyboard.
- Sufficient color contrast must be used for all text and UI elements, adhering to WCAG 2.1 AA standards.

# 5.0.0 Business Rules

- {'rule_id': 'BR-001', 'rule_description': "The 'SOW Review' interface can only be accessed if the SOW processing has reached a terminal state ('Processed' or 'Failed').", 'enforcement_point': 'Server-side check upon page load request.', 'violation_handling': "If the status is 'Processing' or 'Pending', the user should be shown a status page indicating processing is underway, not the review form."}

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-030

#### 6.1.1.2 Dependency Reason

An SOW document must be uploaded before it can be processed and its data reviewed.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-032

#### 6.1.2.2 Dependency Reason

The system must have a mechanism to signal the successful completion of the asynchronous SOW processing job to make the data available for review.

### 6.1.3.0 Story Id

#### 6.1.3.1 Story Id

US-033

#### 6.1.3.2 Dependency Reason

The system must handle processing failures and provide the necessary error information that this story's UI needs to display.

## 6.2.0.0 Technical Dependencies

- The backend AI Ingestion Service must be complete and capable of storing the extracted, structured data in the PostgreSQL database.
- A backend API endpoint (`GET /api/v1/projects/{projectId}/sow-data`) must exist to serve the extracted SOW data and sanitized text.

## 6.3.0.0 Data Dependencies

- Requires access to the `Project` and `SOW` data entities, specifically the SOW processing status and the stored extracted data (likely in a JSONB column).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The 'SOW Review' page, including the extracted data and sanitized text, must achieve a Largest Contentful Paint (LCP) of under 2.5 seconds as per REQ-NFR-001.

## 7.2.0.0 Security

- Access to this interface must be strictly limited to users with the 'System Administrator' role, enforced at the API Gateway and service level as per REQ-NFR-003.

## 7.3.0.0 Usability

- The interface should be intuitive, requiring minimal training for an Admin to understand how to verify and correct the AI's output.

## 7.4.0.0 Accessibility

- The UI must comply with Web Content Accessibility Guidelines (WCAG) 2.1 Level AA as per REQ-INT-001.

## 7.5.0.0 Compatibility

- The interface must be fully functional on the latest two major versions of Chrome, Firefox, Safari, and Edge as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Frontend complexity in building a robust, responsive, and accessible human-in-the-loop UI with multiple complex components (tag inputs, rich text editor, dynamic lists).
- Client-side state management to handle form state, validation, and the 'unsaved changes' warning.
- Requires a well-defined data contract between the frontend and the backend API for the structured SOW data.

## 8.3.0.0 Technical Risks

- The chosen rich text editor component may have accessibility or performance issues that need to be mitigated.
- Rendering very large sanitized SOW documents could impact page performance; lazy loading or virtualization might be needed.

## 8.4.0.0 Integration Points

- Frontend client fetches data from the backend Project Service API.
- The UI will trigger actions that call other API endpoints for saving (US-035) and approving (US-036).

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify correct data display for a successfully processed SOW.
- Verify correct error state display for a failed SOW.
- Verify graceful handling of a partially processed SOW.
- Test the 'unsaved changes' modal by editing a field and attempting to navigate away.
- Test the UI's responsiveness across desktop, tablet, and mobile viewport sizes.
- Perform keyboard-only navigation tests to ensure all interactive elements are reachable and operable.

## 9.3.0.0 Test Data Needs

- A project with a successfully processed SOW containing data for all fields.
- A project with a failed SOW processing job, including a mock error message and correlation ID.
- A project with a partially processed SOW (e.g., skills and summary, but no deliverables).

## 9.4.0.0 Testing Tools

- Jest for frontend unit tests.
- Playwright for E2E tests.
- Axe for automated accessibility testing.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing.
- Code reviewed and approved by at least one other engineer.
- Unit and integration tests implemented with >80% code coverage.
- E2E tests for all key scenarios are written and passing in the CI pipeline.
- User interface has been reviewed for UX consistency and adherence to the design system.
- Performance requirements (LCP < 2.5s) have been verified.
- Automated accessibility checks (Axe) pass with zero critical violations.
- Documentation for the UI components and API endpoint has been created/updated.
- Story has been deployed and successfully verified in the staging environment.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This story is a critical enabler for the entire project brief creation and distribution workflow.
- It is blocked by the completion of the backend SOW processing logic.
- It is a blocker for stories related to editing, approving, and distributing the project brief (US-035, US-036, US-042).

## 11.4.0.0 Release Impact

This feature is essential for the Minimum Viable Product (MVP) as it provides the necessary quality control for the core AI automation feature.

