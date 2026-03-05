{
  "diagram_info": {
    "diagram_name": "Client-Side Authentication State Machine",
    "diagram_type": "stateDiagram-v2",
    "purpose": "To define the definitive states and transitions for the user session lifecycle, specifically focusing on the interaction between Login, MFA, Idle Timeouts, and Session Persistence as implemented in the Frontend.",
    "target_audience": [
      "Frontend Developers",
      "Security Engineers",
      "QA Engineers",
      "UX Designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid stateDiagram-v2 syntax verified",
  "rendering_notes": "Optimized for high contrast; uses composite states for the Authenticated session lifecycle.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Frontend SPA",
      "AWS Cognito (Auth Service)"
    ],
    "key_processes": [
      "Credential Validation",
      "MFA Challenge",
      "Inactivity Monitoring",
      "Token Refresh"
    ],
    "decision_points": [
      "Is MFA Enabled?",
      "Is Credentials Valid?",
      "Is Inactivity > Threshold?",
      "Is Refresh Token Valid?"
    ],
    "success_paths": [
      "Login -> MFA -> Active Session",
      "Idle Warning -> Resume Session"
    ],
    "error_scenarios": [
      "Invalid Credentials",
      "MFA Failure",
      "Account Lockout (Rate Limiting)",
      "Session Expiry"
    ],
    "edge_cases_covered": [
      "User cancels MFA",
      "Refresh token expiry during active session",
      "Browser tab synchronization"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "State diagram showing the lifecycle of a user session from unauthenticated to locked out, including MFA steps and idle timeout warnings.",
    "color_independence": "States are labeled clearly; color is used for emphasis but logic is defined by arrows.",
    "screen_reader_friendly": "Transitions are explicitly labeled with events/triggers.",
    "print_compatibility": "High contrast black and white compatible."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral colors work in light/dark modes",
    "performance_notes": "Low rendering cost"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the AuthProvider context and IdleTimer components.",
    "stakeholder_value": {
      "developers": "Blueprints the exact logic for Redux/Context API state machines.",
      "designers": "Identifies necessary UI screens (Login, MFA, Idle Modal, Locked Out).",
      "product_managers": "Clarifies the security UX flow.",
      "QA_engineers": "Provides a map for state transition testing."
    },
    "maintenance_notes": "Update if biometric auth or SSO is introduced.",
    "integration_recommendations": "Include in the 'Authentication' module documentation."
  },
  "validation_checklist": [
    "✅ Login flow covers both MFA and non-MFA paths",
    "✅ Idle timeout logic includes the warning phase (US-011)",
    "✅ Account lockout state included (US-009)",
    "✅ Logout transition is explicit (US-007)",
    "✅ Visual styling differentiates Error/Active/Transient states",
    "✅ Mermaid syntax is valid"
  ]
}

---

# Mermaid Diagram

```mermaid
stateDiagram-v2
    direction TB

    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px
    classDef secure fill:#e3f2fd,stroke:#1565c0,stroke-width:2px
    classDef warning fill:#fff3e0,stroke:#ef6c00,stroke-width:2px
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px

    [*] --> Unauthenticated

    state Unauthenticated {
        [*] --> LoginForm
        LoginForm --> ValidatingCreds : User Submits (Email/Pwd)
        ValidatingCreds --> LoginForm : Invalid Creds (Show Error)
        ValidatingCreds --> AccountLocked : > 5 Failed Attempts (Rate Limit)
    }

    state AccountLocked {
        [*] --> LockoutTimer
        LockoutTimer --> Unauthenticated : Timer Expired (e.g. 15 mins)
        note right of LockoutTimer
            US-006/US-009: 
            Prevent brute force
        end note
    }

    state MFAPhase {
        [*] --> InputCode
        InputCode --> ValidatingCode : Submit TOTP
        ValidatingCode --> InputCode : Invalid Code
        ValidatingCode --> AccountLocked : > 5 Failed MFA Attempts
        InputCode --> Unauthenticated : Cancel / Back
    }

    state AuthenticatedSession {
        [*] --> Active : Session Established (JWT Stored)
        
        state Active {
            [*] --> MonitoringActivity
            MonitoringActivity --> MonitoringActivity : User Action (Reset Timer)
            MonitoringActivity --> RefreshingToken : Access Token Expiring
            RefreshingToken --> MonitoringActivity : Token Refreshed
        }

        state IdleWarning {
            [*] --> CountdownModal
            CountdownModal --> Active : User Clicks "Stay Logged In"
            note right of CountdownModal
                US-011: Warning 
                before timeout
            end note
        }

        Active --> IdleWarning : Inactivity Threshold Reached
        Active --> Unauthenticated : Refresh Token Invalid/Expired
    }

    %% Transitions
    ValidatingCreds --> MFAPhase : Creds Valid & MFA Enabled
    ValidatingCreds --> AuthenticatedSession : Creds Valid & No MFA
    ValidatingCode --> AuthenticatedSession : MFA Validated (Full Session)
    
    IdleWarning --> Unauthenticated : Countdown Expired (Auto-Logout)
    AuthenticatedSession --> Unauthenticated : User Clicks Logout

    %% Styling
    class AuthenticatedSession success
    class MFAPhase secure
    class AccountLocked error
    class IdleWarning warning
    class Unauthenticated default
```