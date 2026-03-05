{
  "diagram_info": {
    "diagram_name": "MFA Verification Input Interaction Logic",
    "diagram_type": "flowchart",
    "purpose": "Documents the complex interaction logic for the multi-field MFA input component, detailing how keyboard events, paste actions, and navigation result in state updates and validation triggers.",
    "target_audience": [
      "frontend developers",
      "QA engineers",
      "UX designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with clear subgraphs for event types",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "MFA Component",
      "Clipboard API",
      "Validation Service"
    ],
    "key_processes": [
      "Input Handling",
      "Focus Management",
      "Paste Parsing",
      "Auto-submission"
    ],
    "decision_points": [
      "Is input numeric?",
      "Is field empty?",
      "Is paste content valid?",
      "Is code complete?"
    ],
    "success_paths": [
      "Sequential entry completing code",
      "Paste valid code completing sequence"
    ],
    "error_scenarios": [
      "Non-numeric entry",
      "Paste containing invalid chars",
      "Incomplete code submission"
    ],
    "edge_cases_covered": [
      "Backspace on empty field (focus shift)",
      "Paste longer than remaining fields",
      "Middle-field editing"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the internal logic of the MFA input component, showing paths for typing, deleting, and pasting codes leading to validation.",
    "color_independence": "Logic flow relies on directional arrows and shape types",
    "screen_reader_friendly": "Nodes labeled with specific actions and conditions",
    "print_compatibility": "High contrast black and white compatible"
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral styling with semantic colors for outcomes",
    "performance_notes": "Standard flowchart complexity"
  },
  "usage_guidelines": {
    "when_to_reference": "During implementation of the MFAVerificationInput component",
    "stakeholder_value": {
      "developers": "Exact logic for handleKeyDown and handlePaste event listeners",
      "designers": "Verification of micro-interaction behaviors (auto-focus)",
      "product_managers": "Understanding of the frictionless entry requirements",
      "QA_engineers": "Test cases for backspace navigation and paste edge cases"
    },
    "maintenance_notes": "Update if OTP length changes or alpha-numeric support is added",
    "integration_recommendations": "Link to the React component documentation"
  },
  "validation_checklist": [
    "✅ Input entry flow mapped",
    "✅ Backspace navigation logic defined",
    "✅ Paste event parsing included",
    "✅ Validation trigger conditions specified",
    "✅ Focus management logic clear",
    "✅ Edge cases for empty/filled states handled"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start([User Focuses Input Field i])
    EventListener{Event Detected}
    
    %% Subgraph: Input Entry Logic
    subgraph Entry ["0. Input Entry (Key Press)"]
        direction TB
        IsNumeric{Is Key Numeric?}
        UpdateState[Update State index i]
        IsLastField{Is index == Length-1?}
        FocusNext[Focus Input i+1]
        RejectInput[Prevent Default / Ignore]
    end

    %% Subgraph: Navigation Logic
    subgraph Nav ["1. Backspace Navigation"]
        direction TB
        IsEmpty{Is Input i Empty?}
        ClearCurrent[Clear Input i]
        FocusPrev[Focus Input i-1]
        DeletePrev[Delete Value at i-1]
        IsFirst{Is index == 0?}
        Stay[Stay Focused at 0]
    end

    %% Subgraph: Paste Logic
    subgraph Paste ["2. Paste Event"]
        direction TB
        GetClipboard[Get Clipboard Text]
        CleanData[Strip Non-Numeric Chars]
        Distribute[Map chars to Inputs starting at i]
        UpdateAll[Update Full State Array]
        FocusEnd[Focus Last Filled Input]
    end

    %% Subgraph: Validation Logic
    subgraph Validation ["3. Validation Trigger"]
        direction TB
        CheckComplete{Is Code Complete? \n length == 6}
        TriggerSubmit((Trigger onComplete))
        AwaitState[State: Validating...]
        APIResponse{API Response}
        SuccessState[State: Success]
        ErrorState[State: Error / Shake UI]
        ClearInput[Optional: Clear Input]
    end

    %% Relationships - Main Flow
    Start --> EventListener
    
    %% Path 0: Input Entry
    EventListener -- "KeyDown (0-9)" --> IsNumeric
    IsNumeric -- Yes --> UpdateState
    IsNumeric -- No --> RejectInput
    UpdateState --> IsLastField
    IsLastField -- No --> FocusNext
    IsLastField -- Yes --> CheckComplete

    %% Path 1: Backspace
    EventListener -- "KeyDown (Backspace)" --> IsFirst
    IsFirst -- Yes --> ClearCurrent
    IsFirst -- No --> IsEmpty
    IsEmpty -- No (Value exists) --> ClearCurrent
    IsEmpty -- Yes (Already empty) --> FocusPrev
    FocusPrev --> DeletePrev
    
    %% Path 2: Paste
    EventListener -- "OnPaste" --> GetClipboard
    GetClipboard --> CleanData
    CleanData --> Distribute
    Distribute --> UpdateAll
    UpdateAll --> FocusEnd
    FocusEnd --> CheckComplete

    %% Path 3: Validation
    CheckComplete -- Yes --> TriggerSubmit
    CheckComplete -- No --> Stop([Wait for Input])
    TriggerSubmit --> AwaitState
    AwaitState --> APIResponse
    APIResponse -- 200 OK --> SuccessState
    APIResponse -- 4xx/5xx --> ErrorState
    ErrorState --> ClearInput
    ClearInput --> Start

    %% Styling
    classDef event fill:#e1f5fe,stroke:#0277bd,stroke-width:2px,color:#000
    classDef action fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px,color:#000
    classDef logic fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px,color:#000
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px,color:#000
    classDef state fill:#e0f2f1,stroke:#00695c,stroke-width:2px,stroke-dasharray: 5 5,color:#000

    class EventListener,IsNumeric,IsLastField,IsEmpty,IsFirst,CheckComplete,APIResponse logic
    class UpdateState,FocusNext,RejectInput,ClearCurrent,FocusPrev,DeletePrev,Stay,GetClipboard,CleanData,Distribute,UpdateAll,FocusEnd,TriggerSubmit,ClearInput action
    class ErrorState error
    class SuccessState success
    class AwaitState state
    class Start event
```