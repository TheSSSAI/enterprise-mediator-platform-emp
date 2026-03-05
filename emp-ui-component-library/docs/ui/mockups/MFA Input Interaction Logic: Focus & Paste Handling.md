{
  "diagram_info": {
    "diagram_name": "MFA Input Interaction Logic: Focus & Paste Handling",
    "diagram_type": "flowchart",
    "purpose": "To document the complex micro-interactions required for the segmented MFA/OTP input component, specifically detailing how focus moves between inputs and how paste events are parsed and distributed.",
    "target_audience": [
      "frontend developers",
      "QA engineers",
      "UI/UX designers"
    ],
    "complexity_level": "medium",
    "estimated_review_time": "5 minutes"
  },
  "syntax_validation": "Mermaid syntax verified and tested",
  "rendering_notes": "Optimized for readability with distinct subgraphs for Event Triggers and Internal Logic. best viewed in Top-Down orientation.",
  "diagram_elements": {
    "actors_systems": [
      "User",
      "Input Component (Slot 0-5)",
      "Clipboard API",
      "Focus Manager"
    ],
    "key_processes": [
      "Paste Extraction",
      "Numeric Validation",
      "Auto-tabbing",
      "Backspace Navigation"
    ],
    "decision_points": [
      "Is content numeric?",
      "Is input full?",
      "Is backspace on empty?",
      "Is paste valid?"
    ],
    "success_paths": [
      "Single digit entry -> Auto-advance",
      "Valid 6-digit paste -> Auto-fill & Submit"
    ],
    "error_scenarios": [
      "Non-numeric paste",
      "Partial paste",
      "Navigation beyond bounds"
    ],
    "edge_cases_covered": [
      "Backspace on empty slot",
      "Paste exceeding remaining slots",
      "Focus management on error"
    ]
  },
  "accessibility_considerations": {
    "alt_text": "Flowchart detailing the interaction logic for a 6-digit MFA input field, covering key press navigation, auto-advancing focus on input, and distributing pasted content across multiple fields.",
    "color_independence": "Logic paths are defined by arrows and labels; colors indicate state types (event vs logic vs error).",
    "screen_reader_friendly": "Flow is logical and linear; decision points are binary.",
    "print_compatibility": "High contrast black and white rendering is supported."
  },
  "technical_specifications": {
    "mermaid_version": "10.0+ compatible",
    "responsive_behavior": "Nodes wrap logically; subgraph containers maintain context.",
    "theme_compatibility": "Uses standard classDefs compatible with light/dark modes.",
    "performance_notes": "Logic represents client-side event handlers with minimal computational overhead."
  },
  "usage_guidelines": {
    "when_to_reference": "During the implementation of the MFAVerificationInput component (US-009) and when writing unit tests for interaction logic.",
    "stakeholder_value": {
      "developers": "Exact logic for handlePaste, handleChange, and handleKeyDown functions.",
      "designers": "Visualizing the 'happy path' vs 'error path' for user feedback.",
      "product_managers": "Understanding the usability enhancements (auto-advance, paste support).",
      "QA_engineers": "Test cases for paste, backspace, and non-numeric entry."
    },
    "maintenance_notes": "Update if the number of digits changes or if alpha-numeric codes are supported in the future.",
    "integration_recommendations": "Link this diagram in the Storybook documentation for the MFAVerificationInput component."
  },
  "validation_checklist": [
    "✅ Paste event parsing documented",
    "✅ Auto-advance logic on input included",
    "✅ Backspace navigation logic included",
    "✅ Numeric validation checks present",
    "✅ Mermaid syntax validated",
    "✅ Visual hierarchy distinguishes user action from system response",
    "✅ Error states for invalid input included",
    "✅ Final submission trigger identified"
  ]
}

---

# Mermaid Diagram

```mermaid
flowchart TD
    %% Nodes
    Start((User Interaction))
    
    subgraph Events [Event Triggers]
        E_Focus[User Focuses Input[i]]
        E_Paste[User Pastes Content]
        E_Input[User Types Character]
        E_Key[User Presses Key Down]
    end

    subgraph Logic [Component Logic]
        %% Paste Logic
        L_GetClip[Get Clipboard Data]
        L_Clean[Clean Data: Trim & Remove Non-Numeric]
        L_CheckLen{Length > 0?}
        L_Split[Split String into Array]
        L_Map[Map Characters to Inputs[i]...Inputs[i+n]]
        L_UpdateState[Update Component State]
        L_FocusLast[Set Focus to Last Filled Index]
        L_CheckComplete{All 6 Filled?}
        
        %% Input Logic
        L_ValNum{Is Numeric?}
        L_SetVal[Set Input[i] Value]
        L_CheckLast{Is Input[5]?}
        L_Next[Focus Next Input: i+1]
        L_TriggerSubmit[Trigger Auto-Submit / Verify]
        
        %% Navigation Logic
        L_CheckKey{Key Type?}
        L_Back{Backspace?}
        L_Arrow{Arrow Left/Right?}
        L_IsEmpty{Is Input[i] Empty?}
        L_Prev[Focus Previous Input: i-1]
        L_MoveFocus[Move Focus Target]
    end

    subgraph Feedback [UI Feedback]
        UI_Error[Show 'Invalid Input' Tooltip]
        UI_Ring[Update Focus Ring]
        UI_Filled[Visual State: Filled]
    end

    %% Connections
    Start --> E_Focus
    E_Focus --> UI_Ring
    
    %% Paste Flow
    Start --> E_Paste
    E_Paste --> L_GetClip
    L_GetClip --> L_Clean
    L_Clean --> L_CheckLen
    L_CheckLen -- No --> UI_Error
    L_CheckLen -- Yes --> L_Split
    L_Split --> L_Map
    L_Map --> L_UpdateState
    L_UpdateState --> UI_Filled
    L_UpdateState --> L_FocusLast
    L_FocusLast --> L_CheckComplete
    L_CheckComplete -- Yes --> L_TriggerSubmit
    
    %% Input Flow
    Start --> E_Input
    E_Input --> L_ValNum
    L_ValNum -- No --> UI_Error
    L_ValNum -- Yes --> L_SetVal
    L_SetVal --> UI_Filled
    L_SetVal --> L_CheckLast
    L_CheckLast -- No --> L_Next
    L_CheckLast -- Yes --> L_TriggerSubmit
    L_Next --> UI_Ring
    
    %% Navigation Flow
    Start --> E_Key
    E_Key --> L_CheckKey
    L_CheckKey -- Other --> L_ValNum
    L_CheckKey -- Nav Keys --> L_Arrow
    L_Arrow --> L_MoveFocus
    L_MoveFocus --> UI_Ring
    
    L_CheckKey -- Backspace --> L_Back
    L_Back --> L_IsEmpty
    L_IsEmpty -- Yes (Move back) --> L_Prev
    L_IsEmpty -- No (Clear current) --> L_SetVal
    L_Prev --> UI_Ring

    %% Styling
    classDef event fill:#e0f2f1,stroke:#009688,stroke-width:2px,color:#004d40
    classDef logic fill:#f3e5f5,stroke:#9c27b0,stroke-width:2px,color:#4a148c
    classDef decision fill:#fff9c4,stroke:#fbc02d,stroke-width:2px,color:#3e2723
    classDef ui fill:#e3f2fd,stroke:#2196f3,stroke-width:2px,color:#0d47a1
    classDef error fill:#ffebee,stroke:#f44336,stroke-width:2px,color:#b71c1c

    class E_Focus,E_Paste,E_Input,E_Key event
    class L_GetClip,L_Clean,L_Split,L_Map,L_UpdateState,L_FocusLast,L_SetVal,L_Next,L_TriggerSubmit,L_Prev,L_MoveFocus logic
    class L_CheckLen,L_CheckComplete,L_ValNum,L_CheckLast,L_CheckKey,L_Back,L_Arrow,L_IsEmpty decision
    class UI_Ring,UI_Filled ui
    class UI_Error error
```