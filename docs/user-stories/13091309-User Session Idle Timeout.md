# 1 Story Metadata

| Property | Value |
|----------|-------|
| Story Id | US-011 |
| Elaboration Date | 2025-01-15 |
| Development Readiness | Complete |

# 2 Story Narrative

| Property | Value |
|----------|-------|
| Title | User Session Idle Timeout |
| As A User Story | As a logged-in user of the Enterprise Mediator Pla... |
| User Persona | Any authenticated user (System Administrator, Fina... |
| Business Value | Enhances system security by mitigating the risk of... |
| Functional Area | Security & Authentication |
| Story Theme | User Account Management |

# 3 Acceptance Criteria

## 3.1 Criteria Id

### 3.1.1 Criteria Id

AC-011-01

### 3.1.2 Scenario

User receives an inactivity warning before session timeout

### 3.1.3 Scenario Type

Happy_Path

### 3.1.4 Given

a user is logged into the application and the configured idle timeout is 15 minutes

### 3.1.5 When

the user has been inactive for 14 minutes

### 3.1.6 Then

a warning modal is displayed, stating 'Your session is about to expire due to inactivity.'

### 3.1.7 And

the modal presents options to 'Stay Logged In' and 'Log Out Now'.

### 3.1.8 Validation Notes

Verify the modal appears at the correct time based on the configured idle duration minus the warning period (60s). The countdown should be visibly decrementing.

## 3.2.0 Criteria Id

### 3.2.1 Criteria Id

AC-011-02

### 3.2.2 Scenario

User extends their session from the inactivity warning

### 3.2.3 Scenario Type

Happy_Path

### 3.2.4 Given

the session inactivity warning modal is displayed

### 3.2.5 When

the user clicks the 'Stay Logged In' button before the countdown expires

### 3.2.6 Then

the warning modal is dismissed.

### 3.2.7 And

the user can continue interacting with the application seamlessly.

### 3.2.8 Validation Notes

After clicking the button, monitor network traffic to ensure a session-extending API call is made. Verify no further warning appears until the new inactivity period is met.

## 3.3.0 Criteria Id

### 3.3.1 Criteria Id

AC-011-03

### 3.3.2 Scenario

User is automatically logged out after ignoring the warning

### 3.3.3 Scenario Type

Happy_Path

### 3.3.4 Given

the session inactivity warning modal is displayed

### 3.3.5 When

the user takes no action and the countdown timer reaches zero

### 3.3.6 Then

the user's session is terminated on the server.

### 3.3.7 And

a message is displayed on the login page: 'You have been logged out due to inactivity.'

### 3.3.8 Validation Notes

Verify the redirection occurs precisely when the timer ends. Check browser storage to confirm session tokens/data are cleared. Any subsequent API call attempts should fail.

## 3.4.0 Criteria Id

### 3.4.1 Criteria Id

AC-011-04

### 3.4.2 Scenario

API calls with an expired session token are rejected

### 3.4.3 Scenario Type

Error_Condition

### 3.4.4 Given

a user's session has been terminated due to inactivity

### 3.4.5 When

the client-side application attempts to make an API call using the expired session token

### 3.4.6 Then

the API server must reject the request with a 401 Unauthorized status code.

### 3.4.7 And

the client-side application must gracefully handle the 401 error by redirecting the user to the login page.

### 3.4.8 Validation Notes

Use browser developer tools to manually trigger an API call after a session has timed out. Confirm the 401 response and the subsequent client-side redirect.

## 3.5.0 Criteria Id

### 3.5.1 Criteria Id

AC-011-05

### 3.5.2 Scenario

Session activity is synchronized across multiple browser tabs

### 3.5.3 Scenario Type

Edge_Case

### 3.5.4 Given

a user is logged in with the application open in two separate browser tabs (Tab A and Tab B)

### 3.5.5 When

the user is actively using the application in Tab A

### 3.5.6 Then

the inactivity timer for the entire session is reset.

### 3.5.7 And

if the user clicks 'Stay Logged In' in a warning modal on Tab A, the warning modal on Tab B (if present) is also dismissed.

### 3.5.8 Validation Notes

This can be tested by setting a short timeout, performing actions in one tab, and observing that the other tab's session remains active. A mechanism like BroadcastChannel or localStorage events should be used for cross-tab communication.

## 3.6.0 Criteria Id

### 3.6.1 Criteria Id

AC-011-06

### 3.6.2 Scenario

System Administrator can configure the idle timeout duration

### 3.6.3 Scenario Type

Alternative_Flow

### 3.6.4 Given

a user with the 'System Administrator' role is logged in

### 3.6.5 When

they navigate to the system configuration settings page

### 3.6.6 Then

they can view and edit the idle session timeout duration in minutes.

### 3.6.7 And

the new value is applied to all subsequent user sessions after saving.

### 3.6.8 Validation Notes

Verify the existence of the configuration UI. Change the value, save, log out, log back in with a test user, and confirm the new timeout duration is in effect.

# 4.0.0 User Interface Requirements

## 4.1.0 Ui Elements

- A modal dialog for the session timeout warning.
- Text within the modal indicating the reason for the warning.
- A visible countdown timer (e.g., '60 seconds remaining').
- A 'Stay Logged In' button (primary action).
- A 'Log Out Now' button (secondary action).
- A notification/toast message on the login page after a timeout occurs.
- An input field in the Admin settings for configuring the timeout duration.

## 4.2.0 User Interactions

- The warning modal should overlay the current view, preventing interaction with the background page until an action is taken.
- Clicking 'Stay Logged In' dismisses the modal and resets the timer.
- Clicking 'Log Out Now' immediately logs the user out and redirects them.
- Any user activity on the page (keypress, click, mouse move) should reset the client-side inactivity timer.

## 4.3.0 Display Requirements

- The countdown must be clear and accurate.
- The message on the login page post-timeout must clearly state the reason for the logout.

## 4.4.0 Accessibility Needs

- The warning modal must adhere to WCAG 2.1 AA standards.
- Focus must be trapped within the modal when it is active.
- All modal elements must be navigable and operable via keyboard.
- The countdown should be announced by screen readers.

# 5.0.0 Business Rules

## 5.1.0 Rule Id

### 5.1.1 Rule Id

BR-SEC-001

### 5.1.2 Rule Description

All authenticated sessions must be subject to an idle timeout.

### 5.1.3 Enforcement Point

Client-side application logic and server-side token validation.

### 5.1.4 Violation Handling

Session is terminated and user is redirected to the login page.

## 5.2.0 Rule Id

### 5.2.1 Rule Id

BR-CFG-001

### 5.2.2 Rule Description

The idle timeout duration must be a configurable system parameter accessible only to System Administrators.

### 5.2.3 Enforcement Point

Role-Based Access Control (RBAC) on the configuration API endpoint and UI.

### 5.2.4 Violation Handling

Unauthorized users attempting to access the setting will be denied.

# 6.0.0 Dependencies

## 6.1.0 Prerequisite Stories

### 6.1.1 Story Id

#### 6.1.1.1 Story Id

US-006

#### 6.1.1.2 Dependency Reason

A user must be able to log in and establish a session before it can time out.

### 6.1.2.0 Story Id

#### 6.1.2.1 Story Id

US-007

#### 6.1.2.2 Dependency Reason

The timeout mechanism will trigger the existing logout functionality to terminate the session.

## 6.2.0.0 Technical Dependencies

- Authentication Service (AWS Cognito): The session management and token lifecycle are managed by this service.
- Frontend State Management (Zustand): Required to manage the timer state and modal visibility across the application.
- Global API Error Handling: A mechanism must exist to catch 401 Unauthorized responses and trigger a global logout/redirect action.

## 6.3.0.0 Data Dependencies

- A configuration value in the database for the idle timeout duration.

## 6.4.0.0 External Dependencies

*No items available*

# 7.0.0.0 Non Functional Requirements

## 7.1.0.0 Performance

- The client-side inactivity tracking should have negligible impact on application performance and responsiveness.

## 7.2.0.0 Security

- This story is a direct implementation of the session management security control specified in REQ-NFR-003.
- Session tokens must be securely cleared from the client (e.g., localStorage, cookies) upon timeout.
- Server-side session/token validation must be strictly enforced on every API request.

## 7.3.0.0 Usability

- The warning modal should be clear and provide sufficient time for the user to respond.
- The timeout mechanism should not interfere with long-running, legitimate user tasks like filling out a large form.

## 7.4.0.0 Accessibility

- All UI components related to this feature must meet WCAG 2.1 Level AA standards (REQ-INT-001).

## 7.5.0.0 Compatibility

- The session timeout mechanism must function consistently across all supported browsers (Chrome, Firefox, Safari, Edge) as per REQ-DEP-001.

# 8.0.0.0 Implementation Considerations

## 8.1.0.0 Complexity Assessment

Medium

## 8.2.0.0 Complexity Factors

- Implementing a reliable cross-tab communication mechanism (e.g., BroadcastChannel API) to synchronize session state and timers.
- Ensuring the client-side timer is accurate and not prone to drift.
- Coordinating client-side state with server-side token expiration, especially when using refresh tokens.
- Requires both frontend and backend development effort.

## 8.3.0.0 Technical Risks

- Inconsistent behavior across different browsers' implementations of background tab throttling, which could affect timer accuracy.
- Race conditions if multiple tabs attempt to refresh a token simultaneously.

## 8.4.0.0 Integration Points

- AWS Cognito for session invalidation.
- Global HTTP client/interceptor on the frontend to handle 401 responses.
- Admin configuration service to fetch and update the timeout value.

# 9.0.0.0 Testing Requirements

## 9.1.0.0 Testing Types

- Unit
- Integration
- E2E

## 9.2.0.0 Test Scenarios

- Verify modal appears at the correct time.
- Verify session extension works.
- Verify automatic logout and redirect works.
- Verify multi-tab synchronization.
- Verify API rejection after timeout.
- Verify admin configuration of timeout value.

## 9.3.0.0 Test Data Needs

- A test environment where the session timeout can be configured to a short duration (e.g., 1 minute) to facilitate efficient testing.
- User accounts with 'System Administrator' and a standard user role.

## 9.4.0.0 Testing Tools

- Jest for unit tests.
- Playwright for end-to-end tests, which will need to handle waiting for time-based events.

# 10.0.0.0 Definition Of Done

- All acceptance criteria validated and passing in a staging environment.
- Code has been peer-reviewed and merged into the main branch.
- Unit tests are written for timer logic and component states, achieving >80% coverage.
- End-to-end tests for session extension and auto-logout scenarios are implemented and passing.
- The feature is verified to work correctly across all supported browsers.
- Accessibility of the warning modal has been verified (WCAG 2.1 AA).
- Backend API endpoints are secured and tested for expired token rejection.
- User documentation regarding the session timeout feature has been created or updated.

# 11.0.0.0 Planning Information

## 11.1.0.0 Story Points

5

## 11.2.0.0 Priority

🔴 High

## 11.3.0.0 Sprint Considerations

- This is a foundational security feature and should be prioritized early in the development cycle.
- Requires coordinated effort between frontend and backend developers.
- E2E testing for time-based features can be complex and may require specific test environment configurations.

## 11.4.0.0 Release Impact

- Improves the security posture of the application for all users upon release.

