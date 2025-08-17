# 🛰️ Martian Robots – Coding Challenge

This project is an implementation of the **Martian Robots** problem, completed as part of a technical interview process.

---

## 📜 Problem Summary

- The world is a **rectangular grid**, defined by an upper-right coordinate (max: `50 50`).  
  The lower-left is always `(0, 0)`.
- Each robot has:
  - A starting **position**: `(x, y)`
  - An **orientation**: `N` (north), `E` (east), `S` (south), or `W` (west)
- Robots follow an **instruction string** consisting of:
  - `L` → Turn left 90° (no movement)  
  - `R` → Turn right 90° (no movement)  
  - `F` → Move forward one grid unit in the current orientation  
- If a robot moves off the grid, it is marked **LOST** and leaves a **scent** on its last valid coordinate/orientation.  
  Future robots ignore forward moves from scented positions.
- **Constraints**:
  - Grid coordinates: `2 ≤ x, y ≤ 50`
  - Instruction string length: `< 100`

---

## Assumptions

- The grid cannot be initialized with either height or width < 2

---

## 🚀 Getting Started

Pull down the project and `cd` into the MartianRobots directory, then

### 🔧 Build
```bash
dotnet build
```

### ▶️ Run
```bash
dotnet run --project MartianRobots
```

### ✅ Run Tests
```bash
dotnet test
```

---

## 📂 Example Input / Output

**Input:**
```
5 3
1 1 E
RFRFRFRF
3 2 N
FRRFLLFFRRFLL
0 3 W
LLFFFLFLFL
```

**Output:**
```
1 1 E
3 3 N LOST
2 3 S
```

---

## 🛠️ Project Notes

- Implemented as a **C#** Console App
- Extensible design – prepared for additional Input Sources
- Unit tests included for core logic
- Used a console UI library (Spectre) for cleaner interaction

---

## 📌 Roadmap / TODO
  
- [ ] Implement Command Pattern for instruction handling  
- [ ] Add more comprehensive test coverage
