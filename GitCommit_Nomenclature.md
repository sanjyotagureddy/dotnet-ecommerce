# Commit Naming and Version Tagging Rules

## Overview

This document outlines the naming conventions for commit messages and how they affect the version tagging in the repository. Adhering to these rules ensures consistent versioning and clear tracking of changes.

## Version Number Format

The version number follows the format: `MAJOR.MINOR.PATCH.BUILD`

- **MAJOR**: Significant changes that may include breaking changes.
- **MINOR**: Backward-compatible functionality or improvements.
- **PATCH**: Backward-compatible bug fixes, minor changes, or small enhancements.
- **BUILD**: Automatically incremented on every push.

## Commit Naming Conventions

Commit messages should follow a specific format to determine how the version number is incremented:

### 1. **MAJOR Version Increment**
   - **Pattern**: `MAJOR: Description of the change`
   - **Examples**:
     - `MAJOR: Introduce breaking API changes`
     - `MAJOR: Redesign core components`
   - **Impact**: Increments the **MAJOR** version by 1. Resets **MINOR**, **PATCH**, and **BUILD** to `0`.

### 2. **MINOR Version Increment**
   - **Pattern**: `MINOR: Description of the change`
   - **Examples**:
     - `MINOR: Add new authentication feature`
     - `MINOR: Improve performance of data processing`
   - **Impact**: Increments the **MINOR** version by 1. Resets **PATCH** and **BUILD** to `0`.

### 3. **PATCH Version Increment**
   - **Pattern**: `feat:`, `fix:`, `feature:`, `bug:`, `chore:`, `refactor: Description of the change`
   - **Examples**:
     - `feat: Add support for new file formats`
     - `fix: Resolve issue with user login`
     - `chore: Update dependencies`
     - `refactor: Simplify the user interface code`
   - **Impact**: Increments the **PATCH** version by 1. Resets **BUILD** to `0`.

### 4. **BUILD Version Increment**
   - **Pattern**: Any commit message not matching the above patterns.
   - **Examples**:
     - `docs: Update README`
     - `test: Add unit tests for new feature`
   - **Impact**: Increments the **BUILD** version by 1. No other version parts are affected.

## Best Practices

- **Consistency**: Ensure commit messages consistently follow the outlined patterns.
- **Clarity**: Make commit messages descriptive and meaningful to reflect the changes made.
- **Atomic Commits**: Break down changes into atomic commits that align with the version increment rules.

## Example Commit Messages and Resulting Tags

1. **Commit Message**: `MAJOR: Redesign user authentication`
   - **Resulting Tag**: `v2.0.0.0` (if the previous tag was `v1.x.x.x`)

2. **Commit Message**: `MINOR: Add support for OAuth`
   - **Resulting Tag**: `v1.1.0.0` (if the previous tag was `v1.0.x.x`)

3. **Commit Message**: `feat: Implement search functionality`
   - **Resulting Tag**: `v1.0.1.0` (if the previous tag was `v1.0.0.x`)

4. **Commit Message**: `docs: Update API documentation`
   - **Resulting Tag**: `v1.0.0.1` (if the previous tag was `v1.0.0.0`)

## Conclusion

Following these commit naming and tagging rules ensures a clear, consistent versioning strategy that aids in tracking changes, managing releases, and maintaining project quality.
