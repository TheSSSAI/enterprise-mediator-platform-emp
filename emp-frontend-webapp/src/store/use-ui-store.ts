import { create } from 'zustand';
import { devtools } from 'zustand/middleware';

/**
 * Interface definition for the global UI state.
 * Manages transient UI elements like sidebars, modals, and responsive menus.
 */
interface UiState {
  // Sidebar State
  isSidebarOpen: boolean;
  isSidebarCollapsed: boolean;
  
  // Mobile Menu State
  isMobileMenuOpen: boolean;
  
  // Active Modal (managed by ID)
  activeModalId: string | null;
  modalProps: Record<string, any>;

  // Actions
  toggleSidebar: () => void;
  collapseSidebar: (collapsed: boolean) => void;
  toggleMobileMenu: () => void;
  closeMobileMenu: () => void;
  openModal: (modalId: string, props?: Record<string, any>) => void;
  closeModal: () => void;
  reset: () => void;
}

/**
 * Zustand store for UI interactions.
 * This store is client-side only and persists transient state during a session navigation.
 */
export const useUiStore = create<UiState>()(
  devtools(
    (set) => ({
      // Initial State
      isSidebarOpen: true, // Desktop default
      isSidebarCollapsed: false,
      isMobileMenuOpen: false,
      activeModalId: null,
      modalProps: {},

      // Sidebar Actions
      toggleSidebar: () => 
        set((state) => ({ isSidebarOpen: !state.isSidebarOpen })),
      
      collapseSidebar: (collapsed: boolean) => 
        set(() => ({ isSidebarCollapsed: collapsed })),

      // Mobile Menu Actions
      toggleMobileMenu: () => 
        set((state) => ({ isMobileMenuOpen: !state.isMobileMenuOpen })),
      
      closeMobileMenu: () => 
        set(() => ({ isMobileMenuOpen: false })),

      // Modal Actions
      openModal: (modalId: string, props = {}) => 
        set(() => ({ 
          activeModalId: modalId, 
          modalProps: props 
        })),
      
      closeModal: () => 
        set(() => ({ 
          activeModalId: null, 
          modalProps: {} 
        })),

      // Reset
      reset: () => 
        set(() => ({
          isSidebarOpen: true,
          isSidebarCollapsed: false,
          isMobileMenuOpen: false,
          activeModalId: null,
          modalProps: {},
        })),
    }),
    { name: 'ui-store' }
  )
);