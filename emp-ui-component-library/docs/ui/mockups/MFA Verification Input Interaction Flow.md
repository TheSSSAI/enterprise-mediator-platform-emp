{
  "diagram_info": {
    "diagram_name": "MFA Verification Input Interaction Flow",
    "diagram_type": "flowchart",
    "purpose": "Documents the detailed user interaction logic, validation states, and error handling for the MFA OTP input component during the login process.",
    "target_audience": [
      "Frontend Developers",
      "UX Designers",
      "QA Engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "User",
      "MFA Input Component",
      "AWS Cognito Service",
      "Rate Limiter"
    ],
    "key_processes": [
      "Input Masking",
      "Auto-Focus",
      "Paste Handling",
      "TOTP Validation",
      "Account Lockout"
    ],
    "decision_points": [
      "Input Length == 6?",
      "Is Numeric?",
      "Paste Valid?",
      "Backend Validation Success?",
      "Max Attempts Exceeded?"
    ],
    "success_paths": [
      "Manual Entry -> Verify -> Dashboard",
      "Paste Code -> Auto Verify -> Dashboard"
    ],
    "error_scenarios": [
      "Invalid Character Entry",
      "Invalid TOTP Code",
      "Expired Code"
    ],
    "edge_cases_covered": [
      "Clipboard Paste",
      "Account Lockout (5 failed attempts)",
      "Network Failure"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart describing the behavior of the MFA input field, including keyboard navigation, error announcements via ARIA live regions, and focus management.",
    "color_independence": "State changes are indicated by text labels and shape changes in addition to color.",
    "screen_reader_friendly": "Includes specific ARIA state updates for invalid codes and lockout messages.",
    "print_compatibility": "High contrast rendering suitable for documentation exports."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral colors with semantic highlighting for states",
    "performance_notes": "Client-side validation should be instantaneous; Backend validation < 250ms"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing the MFAVerificationInput component or designing the Auth screen UX.",
    "stakeholder_value": {
      "developers": "Logic for paste handling and input constraints",
      "designers": "Visual states for error feedback and loading",
      "product_managers": "Confirmation of security policies (lockout rules)",
      "QA_engineers": "Test cases for edge cases like pasting and rate limiting"
    },
    "maintenance_notes": "Update if MFA provider changes from AWS Cognito or if complexity rules change (e.g., 8 digits).",
    "integration_recommendations": "Link to US-009 and US-008 requirements."
  },
  "validation_checklist": [
    "✅ Paste functionality logic included",
    "✅ Rate limiting/Lockout path defined",
    "✅ Validation states (Client vs Server) clearly separated",
    "✅ Mermaid syntax validated",
    "✅ Success and Error paths clearly distinct"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start([User lands on MFA Challenge Page])
    FocusInput[Component Mounts: Auto-focus first digit input]
    
    subgraph UserInteraction ["User Interaction Layer"]
        InputEvent{Input Event Type}
        TypeChar[User types character]
        PasteCode[User pastes content]
        Backspace[User presses Backspace]
    end

    subgraph ClientValidation ["Client-Side Logic"]
        CheckNumeric{Is Numeric?}
        Ignore[Ignore Input]
        FillDigit[Fill Digit & Advance Focus]
        CheckLength{Length == 6?}
        ParsePaste{Clipboard contains \n6 digits?}
        FillAll[Fill all slots]
        ClearSlot[Clear slot & Move Focus Back]
        EnableButton[Enable 'Verify' Button]
        AutoSubmit{Auto-Submit Configured?}
    end

    subgraph ServerValidation ["Server-Side Logic (AWS Cognito)"]
        SubmitAction[User clicks 'Verify' OR Auto-submit]
        LoadingState[State: Loading / Spinner]
        API_Call[POST /verify-totp]
        CheckResponse{Response Code?}
        Success[200 OK: Valid Token]
        Invalid[400: Invalid Code]
        RateLimit[429: Too Many Requests]
    end

    subgraph UI_Feedback ["UI Feedback & State"]
        Redirect[Redirect to Dashboard]
        ShowError[State: Error \nShow 'Invalid Code' msg\nClear Inputs]
        ShowLockout[State: Locked \nShow 'Too many failed attempts'\nDisable Input 15m]
        IncrementFail[Increment Local Fail Counter]
    end

    %% Logic Flow
    Start --> FocusInput
    FocusInput --> InputEvent
    
    InputEvent -- Typing --> TypeChar
    TypeChar --> CheckNumeric
    CheckNumeric -- No --> Ignore
    CheckNumeric -- Yes --> FillDigit
    FillDigit --> CheckLength
    
    InputEvent -- Paste --> PasteCode
    PasteCode --> ParsePaste
    ParsePaste -- No --> Ignore
    ParsePaste -- Yes --> FillAll
    FillAll --> CheckLength

    InputEvent -- Delete --> Backspace
    Backspace --> ClearSlot
    
    CheckLength -- No --> InputEvent
    CheckLength -- Yes --> EnableButton
    EnableButton --> AutoSubmit
    
    AutoSubmit -- No --> InputEvent
    AutoSubmit -- Yes --> SubmitAction
    EnableButton -- Manual Click --> SubmitAction
    
    SubmitAction --> LoadingState
    LoadingState --> API_Call
    API_Call --> CheckResponse
    
    CheckResponse -- Success --> Success
    Success --> Redirect
    
    CheckResponse -- Fail --> Invalid
    Invalid --> IncrementFail
    IncrementFail --> ShowError
    ShowError --> FocusInput
    
    CheckResponse -- Lockout --> RateLimit
    RateLimit --> ShowLockout

    %% Styling
    classDef default fill:#f9f9f9,stroke:#333,stroke-width:1px;
    classDef interaction fill:#e3f2fd,stroke:#2196f3,stroke-width:2px;
    classDef validation fill:#fff9c4,stroke:#fbc02d,stroke-width:2px;
    classDef server fill:#e8eaf6,stroke:#3f51b5,stroke-width:2px;
    classDef success fill:#e8f5e9,stroke:#4caf50,stroke-width:2px;
    classDef error fill:#ffebee,stroke:#f44336,stroke-width:2px;

    class InputEvent,TypeChar,PasteCode,Backspace interaction;
    class CheckNumeric,CheckLength,ParsePaste,AutoSubmit validation;
    class API_Call,CheckResponse,SubmitAction server;
    class Success,Redirect success;
    class Invalid,RateLimit,ShowError,ShowLockout error;
```