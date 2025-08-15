# 🛰️ Martian Robots – Coding Challenge

This project is an implementation of the **Martian Robots** problem, as part of a technical interview process.

---

## 📜 Problem Summary

- The world is a **rectangular grid** defined by an upper-right coordinate (max `50 50`).  
  The lower-left is always `(0, 0)`.
- Multiple robots are individually deployed to this world.
- Each robot has:
  - A starting **position** (x, y)
  - An **orientation**: `N` (north), `E` (east), `S` (south), or `W` (west)
- Robots follow an **instruction string** (`L`, `R`, `F`):
  - `L` → Turn left 90° (no movement)
  - `R` → Turn right 90° (no movement)
  - `F` → Move forward one grid unit in the current orientation
- If a robot moves off the grid, it is marked **LOST** and leaves a **scent** on the last valid coordinate/orientation.  
  Future robots ignore forward moves from scented positions.
- **Constraints**:
  - Grid coordinates: `0 ≤ x, y ≤ 50`
  - Instruction string length: `< 100`

---

## 🚀 How to Run

### **1️⃣ Build**
```bash
dotnet build

dotnet run --project MartianRobots input.txt

---

## Running Tests
```bash
dotnet test

---

## TODO

- readme
- Command pattern for instruction types
- Console library for better UI
- more tests
- gitignore