{
  "diagram_info": {
    "diagram_name": "Client-Side Input Validation State Machine",
    "diagram_type": "flowchart",
    "purpose": "Documents the detailed interaction logic for secure form inputs (e.g., Project Cost, ID numbers) including character-level filtering, paste sanitization, and visual error feedback states.",
    "target_audience": [
      "Frontend Developers",
      "QA Engineers",
      "UX Designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Input Component",
      "Validation Engine",
      "UI State Manager"
    ],
    "key_processes": [
      "Keystroke Filtering",
      "Paste Sanitization",
      "Blur Validation",
      "Error State Rendering"
    ],
    "decision_points": [
      "Is Character Valid?",
      "Is Paste Content Valid?",
      "Is Field Required?"
    ],
    "success_paths": [
      "Valid character entry",
      "Clean paste operation",
      "Successful validation on blur"
    ],
    "error_scenarios": [
      "Invalid character rejected",
      "Paste format mismatch",
      "Validation failure state"
    ],
    "edge_cases_covered": [
      "Non-numeric characters",
      "Formatted content pasting",
      "Empty required fields"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart describing input validation logic: keystrokes are filtered instantly, paste events are sanitized, and invalid states trigger visual and ARIA feedback.",
    "color_independence": "States are distinguished by shape and label, not just color.",
    "screen_reader_friendly": "Includes ARIA-live region updates in the error flow.",
    "print_compatibility": "High contrast rendering suitable for black and white printing."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+",
    "responsive_behavior": "Vertical layout optimized for scrolling",
    "theme_compatibility": "Neutral styling compatible with light/dark modes",
    "performance_notes": "Logic represents synchronous client-side events ( < 16ms)"
  },
  "usage_guidelines": {
    "when_to_reference": "When implementing custom form controls (e.g., CurrencyInput, PhoneInput) requiring strict formatting.",
    "stakeholder_value": {
      "developers": "Exact logic for event handlers (onKeyDown, onPaste, onBlur).",
      "designers": "Definition of visual error states and feedback timing.",
      "product_managers": "Understanding of data integrity enforcement at the source.",
      "qa_engineers": "Test cases for invalid inputs and clipboard operations."
    },
    "maintenance_notes": "Update if validation libraries (e.g., Zod, Yup) change or if new input types are added.",
    "integration_recommendations": "Link to the Design System 'Form Pattern' documentation."
  },
  "validation_checklist": [
    "✅ Invalid character entry path documented",
    "✅ Paste format error logic included",
    "✅ Validation failure visual state clearly defined",
    "✅ Recovery path (correction) included",
    "✅ Accessibility triggers (ARIA) marked",
    "✅ Syntax is valid Mermaid",
    "✅ Visual hierarchy flows logically from user action to system response"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start([User Focuses Input]) --> StateIdle[State: Idle / Focused]
    
    subgraph Interaction_Events [User Interaction Events]
        StateIdle -->|Type Character| EventKey(Event: onKeyDown/Press)
        StateIdle -->|Paste Content| EventPaste(Event: onPaste)
        StateIdle -->|Leave Field| EventBlur(Event: onBlur)
    end

    subgraph Character_Logic [Real-time Character Validation]
        EventKey --> CheckChar{Is Key Allowed?}
        CheckChar -- No: Invalid Char --> BlockInput[Action: preventDefault]
        BlockInput --> UI_Feedback_Char[Trigger: Shake Animation / Tooltip]
        UI_Feedback_Char --> StateIdle
        CheckChar -- Yes --> UpdateValue[Update Input Value]
        UpdateValue --> ClearErrors[Clear Previous Errors]
        ClearErrors --> StateIdle
    end

    subgraph Paste_Logic [Clipboard Sanitization]
        EventPaste --> GetClipboard[Get Clipboard Text]
        GetClipboard --> CheckPaste{Matches Pattern?}
        CheckPaste -- No: Format Error --> StripChars[Action: Strip Invalid Chars]
        StripChars --> CheckRemaining{Content Remaining?}
        CheckRemaining -- No: Empty --> ShowPasteError[Show Toast: 'Invalid Format']
        ShowPasteError --> StateIdle
        CheckRemaining -- Yes --> UpdateValuePaste[Update Model with Sanitized Data]
        UpdateValuePaste --> StateIdle
        CheckPaste -- Yes --> UpdateValuePaste
    end

    subgraph Validation_State [Validation & Visual Feedback]
        EventBlur --> RunValidation{Run Full Validation}
        RunValidation -- Fail --> SetErrorState[State: Error]
        SetErrorState --> VisualUpdate[Visual: Red Border & Icon]
        VisualUpdate --> AriaUpdate[A11y: aria-invalid='true']
        AriaUpdate --> ShowMsg[Show Inline Error Message]
        ShowMsg --> StateError[Wait for Correction]
        
        RunValidation -- Pass --> SetValidState[State: Valid]
        SetValidState --> RemoveVisuals[Remove Error Styles]
        RemoveVisuals --> HideMsg[Hide Error Message]
        HideMsg --> StateValid[Ready for Submit]
    end

    %% Recovery Path
    StateError -->|User Edits| StateIdle

    %% Styling
    classDef state fill:#f5f5f5,stroke:#333,stroke-width:2px
    classDef event fill:#e3f2fd,stroke:#1565c0,stroke-width:1px
    classDef logic fill:#fff3e0,stroke:#ef6c00,stroke-width:1px,stroke-dasharray: 5 5
    classDef error fill:#ffebee,stroke:#c62828,stroke-width:2px
    classDef success fill:#e8f5e9,stroke:#2e7d32,stroke-width:2px

    class Start,StateIdle,StateValid state
    class EventKey,EventPaste,EventBlur event
    class CheckChar,CheckPaste,CheckRemaining,RunValidation,StripChars,UpdateValue,ClearErrors logic
    class BlockInput,UI_Feedback_Char,ShowPasteError,SetErrorState,VisualUpdate,AriaUpdate,ShowMsg,StateError error
    class SetValidState,RemoveVisuals,HideMsg success
```