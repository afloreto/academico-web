namespace Academico.Models;

public class Disciplina
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Codigo { get; set; }
    public string Docente { get; set; }
    public string CargaHoraria { get; set; }
    public string Turno { get; set; }
    public int TotalAulas { get; set; }
    public int AulasMinistradasDadas { get; set; }
    public int QuantidadeAlunos { get; set; }
    public string Horario { get; set; }
    public string LocalAula { get; set; }
    public int AlunoId { get; set; }
    public Aluno? Aluno { get; set; }
}