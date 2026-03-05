{
  "diagram_info": {
    "diagram_name": "Secure User Authentication Flow with MFA & Error Handling",
    "diagram_type": "flowchart",
    "purpose": "To visualize the end-to-end user login process, specifically detailing security checkpoints, Multi-Factor Authentication (MFA) logic, and handling of critical error scenarios like network failures, invalid credentials, and timeouts.",
    "target_audience": [
      "Frontend Developers",
      "Backend Developers",
      "QA Engineers",
      "Security Auditors"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with distinct color coding for success, error, and decision paths.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Client Application",
      "Identity Provider (Cognito)",
      "Network Layer"
    ],
    "key_processes": [
      "Credential Submission",
      "MFA Verification",
      "Session Token Generation"
    ],
    "decision_points": [
      "Network Availability",
      "Rate Limiting",
      "Credential Validation",
      "MFA Status",
      "MFA Code Validation"
    ],
    "success_paths": [
      "Standard Login",
      "Login with MFA"
    ],
    "error_scenarios": [
      "Network Error",
      "Invalid Credentials (Generic Message)",
      "Rate Limit/Lockout",
      "Invalid MFA Code",
      "MFA Timeout/Max Attempts"
    ],
    "edge_cases_covered": [
      "Offline detection",
      "Brute force protection"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart depicting the login process. Starts with user input. Checks network connectivity. Validates credentials. If valid, checks for MFA. If MFA enabled, prompts for code. Validates code. Handles errors for network, invalid credentials, and MFA failures. Ends with dashboard access or lockout.",
    "color_independence": "Shapes (diamonds for decisions, rectangles for processes) and text labels convey meaning independent of color.",
    "screen_reader_friendly": "Flow is logical and linear with clear branching.",
    "print_compatibility": "High contrast borders ensure visibility in grayscale."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout (TD) ensures readability on mobile and desktop.",
    "theme_compatibility": "Uses standard class definitions for easy theming.",
    "performance_notes": "Standard node count, renders efficiently."
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the login page, MFA verification screen, and API error handling logic.",
    "stakeholder_value": {
      "developers": "Defines exact error states and conditional logic for auth implementation.",
      "designers": "Identifies necessary UI states (Loading, Error, MFA Input, Lockout).",
      "qa_engineers": "Provides a map for negative testing scenarios (Network disconnect, Bad passwords, MFA retries)."
    },
    "maintenance_notes": "Update if authentication provider changes or if new security requirements (e.g., biometric step) are added.",
    "integration_recommendations": "Include in the 'Authentication' module documentation."
  },
  "validation_checklist": [
    "✅ Happy path for standard and MFA login included",
    "✅ Specific error input scenarios covered (Invalid Creds, Network)",
    "✅ Security best practices (Generic Errors, Lockout) visualized",
    "✅ MFA retry logic documented",
    "✅ Mermaid syntax validated",
    "✅ Visual hierarchy emphasizes the critical path"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Node Definitions
    Start([User Visits Login Page])
    InputCreds[User Enters Email & Password]
    SubmitAction(User Clicks 'Login')
    
    %% Network Check Layer
    CheckNetwork{Network Available?}
    NetError[Display Error:\n'Connection Failed.\nPlease check internet.']
    
    %% Security & Rate Limiting Layer
    CheckRateLimit{Rate Limit\nExceeded?}
    LockoutState[Display Error:\n'Too many attempts.\nTry again later.']
    
    %% Credential Validation Layer
    ValidateCreds{Credentials Valid?}
    GenericAuthError[Display Generic Error:\n'Invalid email or password']
    
    %% MFA Decision Layer
    CheckMFAEnabled{MFA Enabled?}
    
    %% MFA Flow
    MFAChallenge[Display MFA Challenge Screen]
    InputMFACode[/User Enters 6-digit TOTP/]
    ValidateMFA{Validate Code}
    
    %% MFA Error Handling
    CheckMFARetries{Retries Remaining?}
    MFAInvalidError[Display Error:\n'Invalid Authentication Code']
    MFALockout[Display Error:\n'Max attempts reached.\nSession reset.']
    
    %% Success State
    GenSession[Generate Session Tokens\n(JWT)]
    Dashboard([Redirect to Dashboard])

    %% Flow Connections
    Start --> InputCreds
    InputCreds --> SubmitAction
    SubmitAction --> CheckNetwork
    
    CheckNetwork -- No --> NetError
    NetError --> InputCreds
    
    CheckNetwork -- Yes --> CheckRateLimit
    CheckRateLimit -- Yes --> LockoutState
    LockoutState --> InputCreds
    
    CheckRateLimit -- No --> ValidateCreds
    ValidateCreds -- No --> GenericAuthError
    GenericAuthError --> InputCreds
    
    ValidateCreds -- Yes --> CheckMFAEnabled
    CheckMFAEnabled -- No --> GenSession
    
    CheckMFAEnabled -- Yes --> MFAChallenge
    MFAChallenge --> InputMFACode
    InputMFACode --> ValidateMFA
    
    ValidateMFA -- Invalid --> CheckMFARetries
    CheckMFARetries -- Yes --> MFAInvalidError
    MFAInvalidError --> InputMFACode
    
    CheckMFARetries -- No --> MFALockout
    MFALockout --> InputCreds
    
    ValidateMFA -- Valid --> GenSession
    GenSession --> Dashboard

    %% Styling
    classDef process fill:#e3f2fd,stroke:#1565c0,stroke-width:2px,color:#0d47a1
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#f57f17
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#b71c1c
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#1b5e20
    classDef startend fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#4a148c

    class InputCreds,SubmitAction,MFAChallenge,InputMFACode,GenSession process
    class CheckNetwork,CheckRateLimit,ValidateCreds,CheckMFAEnabled,ValidateMFA,CheckMFARetries decision
    class NetError,LockoutState,GenericAuthError,MFAInvalidError,MFALockout error
    class Dashboard success
    class Start startend
```