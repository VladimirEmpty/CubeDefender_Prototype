using UnityEngine;
using UnityEngine.SceneManagement;

using CubeDefender.GameSettings;
using CubeDefender.Builder;
using CubeDefender.Service;

namespace CubeDefender
{
    /// <summary>
    /// ������ �� ���������� ������� Bootstrap. ���������� ��������� ��������� �������� �������� � �������� ������� ������� �����.
    /// �������� ��������, ��� ��������� �������� �������� ��������� � �������������� Liquid Builder, ��� �������� ������������ �������� �������� Builder.
    /// </summary>
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CreateGameFieldSetting _createGameFieldSetting;
        [SerializeField] private GameResourcesSetting _gameResourcesSetting;
        [SerializeField] private GameCommonSetting _gameCommonSetting;

        private void Start()
        {
            SceneManager.sceneLoaded += StartGame;
            SceneManager.LoadScene("Game");
        }

        private void StartGame(Scene scene, LoadSceneMode mode)
        {
            new GameResourcesBuilder()
                .WhereGameResourcesSetting(_gameResourcesSetting)
                .Build();

            new GameStartdBuilder()
                .WhereGameFieldSetting(_createGameFieldSetting)
                .Build();

            new GameCoreBuilder()
                .WhereGameCommonSetting(_gameCommonSetting)
                .Build();

            new GameFieldVisualDebugBuilder<EnemyGameFieldService>().Build();
            //new GameFieldVisualDebugBuilder<PlayerGameFieldService>().Build();            
            new GameFieldPlayerTileBuilder()
                .WherePlayerTilePrefab(_gameResourcesSetting.PlayerTile)
                .Build();

            new CreateChipBuilder().Build();

            SceneManager.sceneLoaded -= StartGame;
        }
    }
}

