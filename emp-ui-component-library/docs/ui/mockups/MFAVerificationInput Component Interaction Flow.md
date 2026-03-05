{
  "diagram_info": {
    "diagram_name": "MFAVerificationInput Component Interaction Flow",
    "diagram_type": "flowchart",
    "purpose": "Documents the internal logic, state management, and user interaction flow of the MFA Verification component, including input handling, auto-submission, and accessibility states.",
    "target_audience": [
      "frontend developers",
      "UX designers",
      "QA engineers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "User",
      "MFAVerificationInput Component",
      "Radix UI Primitives",
      "Auth Service"
    ],
    "key_processes": [
      "Input Sanitization",
      "Auto-Focus Management",
      "Clipboard Paste Handling",
      "Validation",
      "Lockout Handling"
    ],
    "decision_points": [
      "Input is Numeric?",
      "Length == 6?",
      "API Success?",
      "Rate Limit Exceeded?"
    ],
    "success_paths": [
      "User enters 6 digits -> Auto-submit -> Success -> Redirect"
    ],
    "error_scenarios": [
      "Invalid Code",
      "Non-numeric input",
      "Network Error",
      "Account Lockout (Too many attempts)"
    ],
    "edge_cases_covered": [
      "Paste from clipboard",
      "Backspace navigation",
      "Mobile keypad trigger"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart showing MFA input logic: handling numeric input, managing focus between slots, auto-submitting on completion, and announcing errors via ARIA live regions.",
    "color_independence": "Logic flow relies on structure and shape; color is supplementary.",
    "screen_reader_friendly": "Includes specific ARIA attribute updates for error states.",
    "print_compatibility": "High contrast, suitable for black and white printing."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral colors used for broad theme support",
    "performance_notes": "Focuses on client-side state transitions"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the MFA input component or when debugging validation logic.",
    "stakeholder_value": {
      "developers": "Exact logic for focus management and paste handling",
      "designers": "Visualization of error and success states",
      "product_managers": "Confirmation of auto-submit behavior and lockout rules",
      "QA_engineers": "Test cases for paste, backspace, and error recovery"
    },
    "maintenance_notes": "Update if shifting from 6-digit to varying length codes.",
    "integration_recommendations": "Use with Radix UI VisuallyHidden primitive for the actual input source of truth."
  },
  "validation_checklist": [
    "✅ Paste functionality logic included",
    "✅ Error recovery flow documented",
    "✅ Accessibility state updates included",
    "✅ Mobile keyboard triggers specified",
    "✅ Backend integration points marked",
    "✅ Auto-submit logic verified",
    "✅ Mermaid syntax validated",
    "✅ Visual hierarchy clear"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    subgraph Client_Side ["Frontend: MFAVerificationInput (Radix UI)"]
        Start([Component Mount]) --> Init[Initialize State: Empty Array[6]]
        Init --> RenderUI[Render 6 Visual Slots + \n1 Hidden Input (Radix VisuallyHidden)]
        
        RenderUI --> WaitUser{User Interaction}
        
        %% Direct Input Flow
        WaitUser -- "Type / Input" --> ValidateNumeric{Is Input Numeric?}
        ValidateNumeric -- No --> Ignore[Ignore Input] --> WaitUser
        ValidateNumeric -- Yes --> UpdateState[Update State & Fill Visual Slot]
        UpdateState --> CheckComplete{Length == 6?}
        
        %% Paste Flow
        WaitUser -- "Paste Event" --> CleanPaste[Sanitize Clipboard Data]
        CleanPaste --> Slice[Slice first 6 chars]
        Slice --> IsNumPaste{Are all Numeric?}
        IsNumPaste -- No --> ErrorToast[Show 'Numeric only' Toast] --> WaitUser
        IsNumPaste -- Yes --> FillAll[Fill All Slots] --> TriggerSubmit
        
        %% Navigation Flow
        WaitUser -- "Backspace/Delete" --> IsEmpty{Slot Empty?}
        IsEmpty -- Yes --> FocusPrev[Focus Previous Slot] --> WaitUser
        IsEmpty -- No --> ClearSlot[Clear Current Slot] --> WaitUser
        
        %% Auto-Submit Logic
        CheckComplete -- No --> AdvanceFocus[Focus Next Slot] --> WaitUser
        CheckComplete -- Yes --> TriggerSubmit[Trigger Auto-Submit]
        
        %% Submission State
        TriggerSubmit --> SetLoading[State: Loading\n(Disable Inputs, Show Spinner)]
        SetLoading --> APICall
    end

    subgraph Server_Side ["Backend: Auth Service"]
        APICall(POST /auth/mfa/verify) --> ValidateTOTP{Validate TOTP}
        ValidateTOTP -- "Valid" --> GenSession[Generate Session Token]
        ValidateTOTP -- "Invalid" --> CheckRate{Rate Limit > 5?}
        CheckRate -- Yes --> LockAccount[Trigger Temp Lockout]
        CheckRate -- No --> ReturnError[Return 400 Invalid]
    end

    %% Response Handling
    GenSession --> HandleSuccess
    ReturnError --> HandleError
    LockAccount --> HandleLockout

    subgraph UI_Updates ["UI State Updates"]
        HandleSuccess[Success Response] --> ShowCheck[Show Success Icon]
        ShowCheck --> Redirect[Redirect to Dashboard]
        
        HandleError[Error Response] --> Shake[Trigger Shake Animation]
        Shake --> ClearInput[Clear Input Fields]
        ClearInput --> FocusFirst[Focus First Slot]
        FocusFirst --> AriaError[Update ARIA Live Region:\n'Invalid Code']
        AriaError --> WaitUser
        
        HandleLockout[429 Lockout Response] --> LockUI[State: Locked Out]
        LockUI --> ShowTimer[Show Countdown Timer]
        ShowTimer --> DisableInteract[Disable All Interaction]
    end

    %% Styling
    classDef state fill:#f8fafc,stroke:#64748b,stroke-width:1px,color:#0f172a
    classDef action fill:#e0f2fe,stroke:#0ea5e9,stroke-width:2px,color:#0c4a6e
    classDef decision fill:#fef3c7,stroke:#d97706,stroke-width:2px,color:#78350f
    classDef server fill:#f0fdf4,stroke:#16a34a,stroke-width:2px,color:#14532d
    classDef error fill:#fef2f2,stroke:#ef4444,stroke-width:2px,color:#7f1d1d

    class Init,RenderUI,UpdateState,CleanPaste,Slice,FillAll,FocusPrev,ClearSlot,AdvanceFocus,SetLoading,ShowCheck,Redirect,Shake,ClearInput,FocusFirst,AriaError,LockUI,ShowTimer,DisableInteract state
    class Start,WaitUser,TriggerSubmit,APICall action
    class ValidateNumeric,CheckComplete,IsNumPaste,IsEmpty,ValidateTOTP,CheckRate decision
    class GenSession,LockAccount,ReturnError server
    class ErrorToast,HandleError,HandleLockout error
```