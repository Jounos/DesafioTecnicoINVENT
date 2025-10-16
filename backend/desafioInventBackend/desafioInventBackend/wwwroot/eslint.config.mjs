import js from "@eslint/js";
import globals from "globals";
import stylisticJs from '@stylistic/eslint-plugin-js';
import { defineConfig, globalIgnores } from "eslint/config";

export default defineConfig(
  globalIgnores([
    "**/test/resources/tests/qunit-junit.js",
    "**/test/resources/tests/sinon.js",
    "**/test/resources/tests/sinon-qunit.js",
    "**/test/resources/tests/qunit-2.js",
    "**/test/resources/tests/qunit-coverage.js",
    "**/eslint.config.mjs",
    "**/Component.js",
    "**/GruntFile.js",
    "**/DialogGenerator.js",
    "**/Jornada*.js",
  ]),
  {
  ...js.configs.recommended,
  languageOptions: {
    globals: {
      ...globals.browser,
      sap: "readonly"
    },
    ecmaVersion: 2023,
    sourceType: "script"
  },
  plugins: {
    "@stylistic/js": stylisticJs
  },
  rules: {
    "prefer-const": 2,
    "no-var": 1,
    "no-shadow": 2,
    "dot-notation": 0,
    "eqeqeq": 1,
    "no-underscore-dangle": 0,
    "wrap-iife": [0, "any"],
    "consistent-this": 2,
    "@stylistic/js/quotes": [1, "single"],
    "@stylistic/js/eol-last": 0,
    "@stylistic/js/no-mixed-spaces-and-tabs": 1,
    "@stylistic/js/no-floating-decimal": 2,
    "@stylistic/js/brace-style": [2, "1tbs", { allowSingleLine: true }],
    "@stylistic/js/space-unary-ops": [2, { "words": false, "nonwords": false }],
    "@stylistic/js/keyword-spacing": [2, { "before": true, "after": true }],
    "@stylistic/js/no-trailing-spaces": 1,
    "@stylistic/js/key-spacing": 1,
    "@stylistic/js/comma-spacing": 1,
    "@stylistic/js/no-multi-spaces": 1,
    "@stylistic/js/semi": 2,
    "no-unused-vars": 0,
    "no-undef": 1,
    "no-div-regex": 2,
    "no-self-compare": 2,
    "no-nested-ternary": 0,
    "radix": [2, "as-needed"],
    "camelcase": 1,
    "max-nested-callbacks": 0,
    "new-cap": 1,
    "no-extra-boolean-cast": [2, { "enforceForInnerExpressions": true }],
    "no-lonely-if": 1,
    "no-new": 2,
    "no-new-wrappers": 0,
    "no-redeclare": 2,
    "no-case-declarations": 0,
    "no-unused-expressions": [1, { "allowShortCircuit": true, "allowTernary": true }],
    "no-use-before-define": 1,
    "default-case": 1,
    "no-irregular-whitespace": 1,
    "no-const-assign": 2,
    "space-before-function-paren": 1
  }
});
