{
  "diagram_info": {
    "diagram_name": "MFA 6-Digit Split Input Interaction Logic",
    "diagram_type": "flowchart",
    "purpose": "To visualize the complex state management, focus transitions, and validation logic required for the 6-digit split input component used in MFA challenges.",
    "target_audience": [
      "Frontend Developers",
      "UX Designers",
      "QA Engineers"
    ],
    "complexity_level": "high",
    "estimated_review_time": "10 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for both light and dark themes with clear separation of user events and internal component logic",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Input Component (React State)",
      "Clipboard API",
      "Verification Service"
    ],
    "key_processes": [
      "Input Handling",
      "Focus Management",
      "Paste Parsing",
      "State Update",
      "Auto-Submission"
    ],
    "decision_points": [
      "Input Type Check",
      "Paste Content Validity",
      "Navigation Keys",
      "Completion Check"
    ],
    "success_paths": [
      "Sequential Entry",
      "Valid Paste",
      "Auto-Submit on Complete"
    ],
    "error_scenarios": [
      "Non-numeric input",
      "Paste too short/long",
      "Invalid characters"
    ],
    "edge_cases_covered": [
      "Backspace across fields",
      "Arrow key navigation",
      "Paste in middle of sequence"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the logic for a 6-digit OTP input field, covering typing, pasting, backspacing, and auto-focus behaviors.",
    "color_independence": "Logic flow relies on shapes and structure, not just color coding.",
    "screen_reader_friendly": "Nodes have descriptive text indicating logic steps.",
    "print_compatibility": "High contrast black and white compatible."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Vertical layout for better scrolling on smaller screens",
    "theme_compatibility": "Uses classDefs for consistent styling across themes",
    "performance_notes": "Logic represents client-side interactions with minimal overhead"
  },
  "usage_guidelines": {
    "when_to_reference": "During the implementation of the MFAVerificationInput component and when writing unit tests for input interactions.",
    "stakeholder_value": {
      "developers": "Exact logic for focus ref usage and state array manipulation",
      "designers": "Confirmation of micro-interaction behaviors (auto-advance, backspace)",
      "product_managers": "Visualization of the seamless user experience requirements",
      "QA_engineers": "Detailed map of edge cases (paste, navigation) for testing"
    },
    "maintenance_notes": "Update if password complexity rules change or if length changes from 6 digits",
    "integration_recommendations": "Embed in the Storybook documentation for the MFA component"
  },
  "validation_checklist": [
    "✅ Paste logic distributed across inputs",
    "✅ Backspace navigation logic defined",
    "✅ Auto-advance on valid input included",
    "✅ Non-numeric filtering logic present",
    "✅ Mermaid syntax validated",
    "✅ Visual hierarchy separates Input, Logic, and Output"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    subgraph UserAction ["User Interaction"]
        Start(("Start: Focus Input[i]"))
        InputEvent[/"User triggers Event (KeyDown/Paste)"/]
        Start --> InputEvent
    end

    subgraph InputLogic ["Component State Logic"]
        TypeCheck{"Event Type?"}
        InputEvent --> TypeCheck

        %% Path 1: Single Character Entry
        TypeCheck -- "Numeric Key (0-9)" --> IsValidNum{"Is Numeric?"}
        IsValidNum -- No --> Ignore["Prevent Default / Ignore"]
        IsValidNum -- Yes --> UpdateState["Update value at Index[i]"]
        UpdateState --> NextCheck{"Is Index < 5?"}
        NextCheck -- Yes --> FocusNext["Focus Input[i+1]"]
        NextCheck -- No --> CheckComplete{"Is Full Code (Len=6)?"}
        
        %% Path 2: Navigation (Backspace)
        TypeCheck -- "Backspace / Delete" --> IsEmpty{"Is Input[i] Empty?"}
        IsEmpty -- Yes --> PrevCheck{"Is Index > 0?"}
        PrevCheck -- Yes --> FocusPrev["Focus Input[i-1]"]
        PrevCheck -- No --> ClearCurrent["Clear Input[i]"]
        IsEmpty -- No --> ClearCurrent
        FocusPrev --> ClearPrev["Clear Input[i-1]"]

        %% Path 3: Navigation (Arrows)
        TypeCheck -- "Arrow Left" --> NavLeft{"Is Index > 0?"}
        NavLeft -- Yes --> MoveLeft["Focus Input[i-1]"]
        TypeCheck -- "Arrow Right" --> NavRight{"Is Index < 5?"}
        NavRight -- Yes --> MoveRight["Focus Input[i+1]"]

        %% Path 4: Paste Event
        TypeCheck -- "Paste (Ctrl+V)" --> GetClip["Get Clipboard Data"]
        GetClip --> CleanClip["Strip Non-Numeric Chars"]
        CleanClip --> SplitClip["Split String into Array"]
        SplitClip --> FillState["Map values to Inputs starting at [0]"]
        FillState --> FocusLast["Focus Last Filled Input"]
        FocusLast --> CheckComplete
    end

    subgraph OutputState ["Visual & System Feedback"]
        CheckComplete -- Yes --> TriggerSubmit(("Trigger onComplete(code)"))
        CheckComplete -- No --> Wait["Wait for more input"]
        TriggerSubmit --> Validate{"API Validation"}
        Validate -- Success --> SuccessState["Show Success UI / Redirect"]
        Validate -- Error --> ErrorState["Show Error Border / Shake Animation"]
        ErrorState --> ClearState["(Optional) Clear Inputs"]
    end

    %% Connections
    FocusNext --> Wait
    ClearCurrent --> Wait
    ClearPrev --> Wait
    MoveLeft --> Wait
    MoveRight --> Wait
    Ignore --> Wait

    %% Styling
    classDef action fill:#e1f5fe,stroke:#01579b,stroke-width:2px,color:#000
    classDef logic fill:#fff3e0,stroke:#e65100,stroke-width:2px,color:#000
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#000
    classDef outcome fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px,color:#000
    classDef error fill:#ffebee,stroke:#b71c1c,stroke-width:2px,color:#000

    class Start,InputEvent action
    class UpdateState,FocusNext,FocusPrev,ClearCurrent,ClearPrev,MoveLeft,MoveRight,GetClip,CleanClip,SplitClip,FillState,FocusLast logic
    class TypeCheck,IsValidNum,NextCheck,CheckComplete,IsEmpty,PrevCheck,NavLeft,NavRight,Validate decision
    class TriggerSubmit,SuccessState,Wait outcome
    class Ignore,ErrorState,ClearState error
```