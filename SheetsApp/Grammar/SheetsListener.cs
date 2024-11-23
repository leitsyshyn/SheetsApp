//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.13.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Sheets.g4 by ANTLR 4.13.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="SheetsParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.13.2")]
[System.CLSCompliant(false)]
public interface ISheetsListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>IncrementExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIncrementExpr([NotNull] SheetsParser.IncrementExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>IncrementExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIncrementExpr([NotNull] SheetsParser.IncrementExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>AffirmExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAffirmExpr([NotNull] SheetsParser.AffirmExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AffirmExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAffirmExpr([NotNull] SheetsParser.AffirmExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>PowerExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPowerExpr([NotNull] SheetsParser.PowerExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>PowerExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPowerExpr([NotNull] SheetsParser.PowerExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>MultiplyExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterMultiplyExpr([NotNull] SheetsParser.MultiplyExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>MultiplyExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitMultiplyExpr([NotNull] SheetsParser.MultiplyExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>CellExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCellExpr([NotNull] SheetsParser.CellExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>CellExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCellExpr([NotNull] SheetsParser.CellExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>NumberExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumberExpr([NotNull] SheetsParser.NumberExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>NumberExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumberExpr([NotNull] SheetsParser.NumberExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>AddExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAddExpr([NotNull] SheetsParser.AddExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>AddExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAddExpr([NotNull] SheetsParser.AddExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>SubtractExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSubtractExpr([NotNull] SheetsParser.SubtractExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>SubtractExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSubtractExpr([NotNull] SheetsParser.SubtractExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>ParenExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterParenExpr([NotNull] SheetsParser.ParenExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>ParenExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitParenExpr([NotNull] SheetsParser.ParenExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DecrementExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDecrementExpr([NotNull] SheetsParser.DecrementExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DecrementExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDecrementExpr([NotNull] SheetsParser.DecrementExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>NegateExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNegateExpr([NotNull] SheetsParser.NegateExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>NegateExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNegateExpr([NotNull] SheetsParser.NegateExprContext context);
	/// <summary>
	/// Enter a parse tree produced by the <c>DivideExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterDivideExpr([NotNull] SheetsParser.DivideExprContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>DivideExpr</c>
	/// labeled alternative in <see cref="SheetsParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitDivideExpr([NotNull] SheetsParser.DivideExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SheetsParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] SheetsParser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SheetsParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] SheetsParser.NumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="SheetsParser.cell"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCell([NotNull] SheetsParser.CellContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="SheetsParser.cell"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCell([NotNull] SheetsParser.CellContext context);
}
