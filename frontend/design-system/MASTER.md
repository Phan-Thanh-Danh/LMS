# LMS Frontend Design System (Master)

> [!NOTE]
> This design system is generated based on the **UI/UX Pro Max Skill** reasoning engine. 
> It provides a consistent framework for all UI/UX development in this project.

## Core Identity
- **Product Type**: Educational LMS / ERP Utility
- **Primary Design Style**: **Bento Box Grid** Core + **Glassmorphism** Overlays
- **Philosophy**: Professional, Data-Rich, Intuitive, and "Premium" Aesthetics.

## Color Palette (Industry: Education)
| Role | Color (Hex) | Purpose |
|------|-------------|---------|
| **Primary** | `#3B82F6` (Electric Blue) | Brand, primary CTAs, active states |
| **Secondary** | `#10B981` (Emerald) | Progress success, completed tasks |
| **Danger** | `#EF4444` (Rose Red) | Alerts, failing grades, rejection |
| **Warning** | `#F59E0B` (Amber) | Pending approvals, cautionary info |
| **Background** | `#F3F4F6` (Soft Grey) | Main canvas for the Bento cards |
| **Surface** | `#FFFFFF` (Pure White) | Card content, elevated surfaces |
| **Text (Body)** | `#111827` (Dark Slate) | Primary readability |
| **Text (Muted)**| `#6B7280` (Cool Grey) | Meta info, breadcrumbs, subtexts |

## Typography (System & Brand)
- **Primary Font**: [Inter](https://fonts.google.com/specimen/Inter) (Variable)
  - *Mood:* Reliable, professional, highly readable at small sizes.
- **Display Font**: [Playfair Display](https://fonts.google.com/specimen/Playfair+Display) (Serif)
  - *Mood:* Academic, prestigious, premium (Used for large titles/certificates).

## Key UI Patterns & Effects
- **Main Dashboard**: **Bento Box Grid** with varied card spans (`1x1`, `2x1`, `2x2`).
  - `border-radius: 1.5rem (24px)`
  - `box-shadow: 0 4px 6px -1px rgb(0 0 0 / 0.1)` (Subtle, modern)
- **Overlays/Modals**: **Glassmorphism**
  - `backdrop-filter: blur(16px) saturate(180%)`
  - `background: rgba(255, 255, 255, 0.7)`
- **Animations**:
  - `duration: 250ms` (Standard UI transitions)
  - `scale-95` on click (Tactile feedback)
  - **Spring Bounce**: For cards and modal entrance.

## UX Guidelines (Anti-Patterns to Avoid)
1. **NO "AI Purple/Pink" Gradients**: Stay grounded in professional blues/greens.
2. **NO Boring Flatness**: Use Z-depth (shadows/blur) to guide the user's eye.
3. **NO Generic Icons**: Use **Lucide-vue-next** for sharp, consistent iconography.
4. **NO Content Jumping**: Use **Skeleton screens** during data fetching (`v-show` with pulse animation).
5. **Touch-Ready**: Ensure all clickable elements meet the **44x44px** minimum.

## Pre-Delivery Checklist
- [ ] WCAG AA Contrast checked (4.5:1 min).
- [ ] Responsive states verified at `375px`, `768px`, `1024px`, `1440px`.
- [ ] Focused state rings visible for keyboard navigation.
- [ ] `prefers-reduced-motion` respected (conditionally disable heavy transitions).
