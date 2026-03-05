# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-089 |
| Elaboration Date | 2025-01-24 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User Manages Notification Preferences |
| As A User Story | As a registered user (Admin, Finance Manager, Clie... |
| User Persona | Any registered and authenticated user of the platf... |
| Business Value | Improves user satisfaction and retention by giving... |
| Functional Area | User Account Management |
| Story Theme | User Profile & Settings |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-001

### 3.1.2 Scenario

User successfully views their notification preferences

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

I am a logged-in user with the role of 'System Administrator'

### 3.1.5 When

I navigate to my account settings and select 'Notification Preferences'

### 3.1.6 Then

I see a page listing all notification types applicable to my role, each with a clear description and an 'Email' toggle switch reflecting its current state.

### 3.1.7 Validation Notes

Verify that the list of notifications matches the expected set for a System Admin (e.g., 'New Proposal Submitted', 'Payment Completed').

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-002

### 3.2.2 Scenario

User successfully updates their notification preferences

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

I am on the 'Notification Preferences' page and the 'New Proposal Submitted' email toggle is ON

### 3.2.5 When

I click the toggle to set it to OFF and click the 'Save Changes' button

### 3.2.6 Then

The system saves my preference, the button is temporarily disabled, a success message 'Preferences saved successfully' is displayed, and the toggle remains in the OFF position.

### 3.2.7 Validation Notes

Check the database to confirm the user's preference for this notification type is updated to false. Trigger the corresponding event and verify no email is sent.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-003

### 3.3.2 Scenario

System honors disabled email notification preference

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

I am a System Administrator who has disabled the 'New Proposal Submitted' email notification

### 3.3.5 When

a Vendor submits a new proposal for a project

### 3.3.6 Then

I do not receive an email notification about the new proposal.

### 3.3.7 Validation Notes

This requires an end-to-end test. Verify the notification service checks the user's preference before attempting to send the email.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-004

### 3.4.2 Scenario

User attempts to change a mandatory notification

### 3.4.3 Scenario Type

Edge_Case

### 3.4.4 Given

I am on the 'Notification Preferences' page

### 3.4.5 When

I locate a mandatory notification, such as 'Password Reset Request' or 'Critical Security Alert'

### 3.4.6 Then

the toggle switch for this notification is disabled (greyed out) and I cannot change its state. Hovering over the toggle displays a tooltip explaining 'This is a mandatory notification and cannot be disabled.'

### 3.4.7 Validation Notes

Verify the toggle element has the 'disabled' attribute and the tooltip appears on hover.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-005

### 3.5.2 Scenario

Saving preferences fails due to a server error

### 3.5.3 Scenario Type

Error_Condition

### 3.5.4 Given

I am on the 'Notification Preferences' page and have changed a setting

### 3.5.5 When

I click 'Save Changes' and the backend API returns a 500 error

### 3.5.6 Then

I see an error message 'Failed to save preferences. Please try again.', and the toggle I changed reverts to its last saved state.

### 3.5.7 Validation Notes

Use browser developer tools to mock a server error and verify the UI handles it gracefully.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-006

### 3.6.2 Scenario

Default preferences for a new user

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

a new Vendor user logs in for the very first time

### 3.6.5 When

they navigate to the 'Notification Preferences' page

### 3.6.6 Then

they see a default set of preferences, with all non-mandatory notifications enabled by default.

### 3.6.7 Validation Notes

Verify that the system applies a default template upon user creation or first login.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A dedicated 'Notification Preferences' page within the user's account settings.
- A list/table of notification types.
- A toggle switch component for each preference (e.g., for the 'Email' channel).
- A 'Save Changes' button that is enabled only when there are unsaved changes.
- A tooltip component for disabled toggles.
- Toast/inline notifications for success and error messages.

## 4.2.0 User Interactions

- User clicks a toggle to change its state from on to off, or vice-versa.
- User clicks 'Save Changes' to persist their settings.
- User hovers over a disabled toggle to see an explanatory tooltip.

## 4.3.0 Display Requirements

- Each preference must have a clear, user-friendly name (e.g., 'Project Brief Received').
- Each preference must have a short description of the event that triggers it (e.g., 'When you are invited to submit a proposal for a new project.').

## 4.4.0 Accessibility Needs

- The page must comply with WCAG 2.1 Level AA standards.
- All interactive elements (toggles, buttons) must be keyboard-focusable and operable.
- Toggle switches must use appropriate ARIA roles (e.g., `role="switch"`) and states (`aria-checked`).
- Success and error messages must be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-001

### 5.1.2 Rule Description

Certain system notifications are mandatory and cannot be disabled by the user.

### 5.1.3 Enforcement Point

The UI will display these options as disabled. The backend API will reject any attempt to modify them.

### 5.1.4 Violation Handling

The API will return a 403 Forbidden or 400 Bad Request error if a user attempts to disable a mandatory notification.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-002

### 5.2.2 Rule Description

Notification preferences are specific to a user, not a role. Each user manages their own settings.

### 5.2.3 Enforcement Point

API endpoints for managing preferences must be scoped to the authenticated user (e.g., `/users/me/preferences`).

### 5.2.4 Violation Handling

An attempt to modify another user's preferences will result in a 403 Forbidden error.

## 5.3.0 Rule Id

### 5.3.1 Rule Id

BR-003

### 5.3.2 Rule Description

New users are assigned a default set of notification preferences based on their role, with most optional notifications enabled.

### 5.3.3 Enforcement Point

During the user creation or first login workflow.

### 5.3.4 Violation Handling

N/A - System process.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

User must be able to log in to access account settings.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-001

#### 6.1.2.2 Dependency Reason

The user account creation and invitation process must exist.

## 6.2.0.0 Technical Dependencies

- A centralized Notification Service capable of checking user preferences before sending a notification.
- A User service that provides the authenticated user's ID and role.
- A defined data model/enum for all possible notification types in the system.

## 6.3.0.0 Data Dependencies

- A new database table is required to store user preferences (e.g., `user_notification_preferences` with fields: `user_id`, `notification_type`, `channel`, `is_enabled`).

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The Notification Preferences page must have a Largest Contentful Paint (LCP) of under 2.5 seconds.
- The API call to save preferences must have a 95th percentile (p95) response time of less than 250ms.

## 7.2.0.0 Security

- The API endpoint for managing preferences must be authenticated and authorized, ensuring a user can only modify their own settings.
- User preference data should be considered private and protected at rest.

## 7.3.0.0 Usability

- The page should be intuitive, with clear labels and descriptions, requiring no user training.
- The system must provide clear feedback for user actions (e.g., saving).

## 7.4.0.0 Accessibility

- Must meet WCAG 2.1 Level AA standards.

## 7.5.0.0 Compatibility

- The feature must function correctly on the latest two major versions of Chrome, Firefox, Safari, and Edge.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Requires modification of the core Notification Service to incorporate a preference check before every send operation.
- Requires creation of a new database schema and API endpoints for managing preferences.
- Requires a comprehensive and maintainable list/enum of all notification types across the platform.

## 8.3.0.0 Technical Risks

- Risk of performance degradation in the Notification Service if the preference check is not optimized.
- Risk of missing a notification type, leading to an uncontrollable notification.
- The end-to-end testing is complex, as it involves multiple services and potentially external email delivery systems.

## 8.4.0.0 Integration Points

- User Service (to get user role and ID).
- Notification Service (to check and honor preferences).
- Frontend user profile/settings module.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E
- Accessibility

## 9.2.0.0 Test Scenarios

- Verify preferences are correctly loaded for each user role.
- Verify saving preferences works and persists across sessions.
- Verify that disabling a notification prevents its delivery (E2E).
- Verify that mandatory notifications cannot be disabled.
- Verify UI gracefully handles API errors.
- Verify keyboard navigation and screen reader compatibility.

## 9.3.0.0 Test Data Needs

- Test users for each role (Admin, Finance, Client, Vendor).
- A defined set of default and mandatory notifications for testing.

## 9.4.0.0 Testing Tools

- Jest for unit/integration tests.
- Playwright for end-to-end tests.
- Axe for accessibility scanning.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing
- Code reviewed and approved by team
- Unit tests implemented with >80% coverage and passing
- Integration testing completed successfully
- End-to-end test for honoring a disabled preference is implemented and passing
- User interface reviewed and approved for UX and accessibility
- Performance requirements verified
- Security requirements validated
- Online user documentation for this feature is created/updated
- Story deployed and verified in staging environment

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🟡 Medium

## 11.3.0.0 Sprint Considerations

- Backend work (API, service logic, DB changes) should be prioritized to unblock frontend development.
- Requires coordination between frontend and backend developers.
- The E2E test may require a dedicated test harness or email-catching service (like MailHog) in the test environment.

## 11.4.0.0 Release Impact

Improves user experience for existing notification features. No direct impact on core business workflows but is a key quality-of-life improvement.

